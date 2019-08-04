using Zone.UmbracoMapper.V8;
using Rasolo.Core.Features.Shared.UI;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPageController : BaseContentPageController<BlogPostPage>
	{
		public BlogPostPageController(IUmbracoMapper umbracoMapper, IBaseContentPageViewModelFactory<BlogPostPage> viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
		}
	}
}