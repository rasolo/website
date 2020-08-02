using Rasolo.Core.Features.Shared.Compositions;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogsPage
{
	public class BlogsPageController : BaseContentPageController<BlogsPage>
	{
		public BlogsPageController(IUmbracoMapper umbracoMapper,
			IBlogsPageViewModelFactory viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
		}
	}
}