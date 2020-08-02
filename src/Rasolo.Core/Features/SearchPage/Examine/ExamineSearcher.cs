using System;
using Examine;
using NPoco.ArrayExtensions;
using Rasolo.Core.Features.Shared.Constants;
using Umbraco.Core;

namespace Rasolo.Core.Features.SearchPage.Examine
{
	public class ExamineSearcher : IExamineSearcher
	{
		private readonly IExamineManager examineManager;

		public ExamineSearcher(IExamineManager examineManager)
		{
			this.examineManager = examineManager;
		}

		public ISearchResults Search(string searchQuery, int maxResults, float fuzzieness, string indexType, string[] nodeTypes, string[] properties)
		{
			if (!examineManager.TryGetIndex(Constants.UmbracoIndexes.ExternalIndexName, out var index))
			{
				throw new InvalidOperationException($"No index found by name {Constants.UmbracoIndexes.ExternalIndexName}");
			}

			var searcher = index.GetSearcher();
			var query = searcher.CreateQuery(indexType);
			var operation = query.GroupedOr(new[] {SearchFields.NodeTypeAlias}, nodeTypes);
			var queryWords = searchQuery.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
			var exactSearchPhrase = new ExactPhraseExamineValue(searchQuery);

			foreach (var word in queryWords)
			{
				operation.And().GroupedOr(properties, word.Fuzzy(fuzzieness), exactSearchPhrase);
			}

			return operation.Execute(maxResults);
		}
	}
}