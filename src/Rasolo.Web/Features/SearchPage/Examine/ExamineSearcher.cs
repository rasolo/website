using System;
using Examine;
using Examine.Search;
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

			var query = index.Searcher.CreateQuery(indexType);
			var operation = query.GroupedOr(new[] { SearchFields.NodeTypeAlias }, nodeTypes);
			var queryWords = searchQuery.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
			operation.Or().GroupedOr(properties, searchQuery.Fuzzy(fuzzieness));
			if (queryWords?.Length > 1)
			{
				var exactSearchPhrase = new ExactPhraseExamineValue(searchQuery);

				foreach (var word in queryWords)
				{
					operation.And().GroupedOr(properties, word.Fuzzy(fuzzieness), exactSearchPhrase);
				}
			}

			return operation.Execute(new QueryOptions(0, maxResults));
		}
	}
}