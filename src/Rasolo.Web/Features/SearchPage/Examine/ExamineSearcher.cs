using System;
using Examine;
using Examine.Search;
using Umbraco.Extensions;
using Rasolo.Web.Features.Shared.Constants.PropertyTypeAlias;
using System.Linq;

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

			var query = index.Searcher.CreateQuery(category: "content");

			var splitSearchQuery = searchQuery.Split(new[] { ' ', '-', '?', '&' }, StringSplitOptions.RemoveEmptyEntries);
			var operation = query.GroupedAnd(new string[] { PropertyTypeAlias.Title }, splitSearchQuery.Select(x => x.MultipleCharacterWildcard()).ToArray());

			return operation.Execute(new QueryOptions(0, maxResults));
		}
	}
}