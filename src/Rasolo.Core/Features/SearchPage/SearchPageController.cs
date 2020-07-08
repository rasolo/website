using Rasolo.Core.Features.Shared.Compositions;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchPageController : BaseContentPageController<SearchPage>
	{
		public SearchPageController(IUmbracoMapper umbracoMapper, ISearchPageViewModelFactory viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
		}
	}
}