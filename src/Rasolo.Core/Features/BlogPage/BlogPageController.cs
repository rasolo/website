using Rasolo.Core.Features.Shared.UI;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPageController : BaseContentPageController<BlogPage>
	{
		public BlogPageController(IUmbracoMapper umbracoMapper, IBaseContentPageViewModelFactory<BlogPage> viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
		}
	}
}