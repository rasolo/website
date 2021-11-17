using Anaximapper;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Rasolo.Web.Features.Shared.Compositions;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Rasolo.Web.Features.BlogsPage
{
	public class BlogsPageController : BaseContentPageController<BlogsPage>
	{
		public BlogsPageController(IPublishedContentMapper anaxiMapper, IBlogsPageViewModelFactory viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, viewModelFactory, logger, compositeViewEngine, umbracoContextAccessor)
		{
		}
	}
}