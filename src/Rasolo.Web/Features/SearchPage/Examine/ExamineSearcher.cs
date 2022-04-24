using System;
using Examine;
using Rasolo.Web.Features.Shared.Constants;

namespace Rasolo.Web.Features.SearchPage.Examine
{
	public class ExamineSearcher : IExamineSearcher
	{
		private readonly IExamineManager _examineManager;

		public ExamineSearcher(IExamineManager examineManager)
		{
			_examineManager = examineManager;
		}

		public ISearchResults Search(string searchQuery, int maxResults, float fuzzieness, string indexType, string[] nodeTypes, string[] properties)
		{
			if (!_examineManager.TryGetIndex("ExternalIndex", out IIndex index))
			{
				throw new InvalidOperationException($"No index found by name {"ExternalIndex"}");
			}
			//restrict the search to the Content section, create our search criteria object
			var query = index.Searcher.CreateQuery(indexType);
			var operation = query.GroupedOr(new[] {SearchFields.NodeTypeAlias}, nodeTypes);
			var queryWords = searchQuery.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
			var exactSearchPhrase = new ExactPhraseExamineValue(searchQuery);

			foreach (var word in queryWords)
			{
				operation.And().GroupedOr(properties, word.Fuzzy(fuzzieness), exactSearchPhrase);
			}

			return operation.Execute();
		}
	}
}