using Examine;

namespace Rasolo.Web.Features.SearchPage.Examine
{
	public interface IExamineSearcher
	{
		ISearchResults Search(string searchQuery, int maxResults, float fuzzieness, string indexType, string[] nodeTypes, string[] properties);
	}
}