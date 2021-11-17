using Anaximapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Rasolo.Web.Features.Shared.Compositions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Rasolo.Web.Features.StartPage
{
	public class StartPageController : RenderController
	{
		public StartPageController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
		{
		}

		

		public IActionResult StartPage()
		{
			return View(new StartPage());
		}


		//public StartPageController
		//	(IPublishedContentMapper anaxiMapper, IStartPageViewModelFactory viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, viewModelFactory, logger, compositeViewEngine, umbracoContextAccessor)
		//{
		//}
	}
}
