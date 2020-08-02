using Examine;

namespace Rasolo.Core.Features.SearchPage.Examine
{
	public interface IExamineSearcher
	{
		ISearchResults Search(string searchQuery, int maxResults, float fuzzieness, string indexType, string[] nodeTypes, string[] properties);
	}
}