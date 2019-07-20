using Rasolo.Core.Features.Shared.Controllers;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPageController : BasePageController<BlogPage>
	{
		public BlogPageController(IUmbracoMapper umbracoMapper) : base(umbracoMapper)
		{
		}
	}
}