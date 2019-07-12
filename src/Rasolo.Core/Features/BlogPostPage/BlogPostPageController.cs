using Rasolo.Core.Features.Shared.Controllers;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPageController : BasePageController<BlogPostPage>
	{
		public BlogPostPageController(IUmbracoMapper umbracoMapper) : base(umbracoMapper)
		{
		}
	}
}