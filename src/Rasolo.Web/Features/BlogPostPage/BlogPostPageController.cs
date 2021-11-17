using Anaximapper;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Rasolo.Web.Features.Shared.Compositions;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Rasolo.Web.Features.BlogPostPage
{
	public class BlogPostPageController : BaseContentPageController<BlogPostPage>
	{
		public BlogPostPageController(IPublishedContentMapper anaxiMapper, IBlogPostPageViewModelFactory viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, viewModelFactory, logger, compositeViewEngine, umbracoContextAccessor)
		{
		}
	}
}