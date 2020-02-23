using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Compositions;
using Zone.UmbracoMapper.V8;
using Umbraco.Web.Models;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPageController : BaseContentPageController<BlogPostPage>
	{
		public BlogPostPageController(IUmbracoMapper umbracoMapper, IBlogPostPageViewModelFactory viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
		}
	}
}