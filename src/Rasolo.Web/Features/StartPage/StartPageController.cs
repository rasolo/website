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
	public class StartPageController : BaseContentPageController<StartPage>
	{
		public StartPageController
			(IPublishedContentMapper anaxiMapper, IStartPageViewModelFactory viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, viewModelFactory, logger, compositeViewEngine, umbracoContextAccessor)
		{
			ViewModelFactory = viewModelFactory;
		}

		public IStartPageViewModelFactory ViewModelFactory { get; }

		public IActionResult StartPage(ContentModel contentModel)
		{
			var mappedModel = this.MapModel(contentModel.Content);
			var viewModel = this.ViewModelFactory.CreateModel(mappedModel, contentModel);

			return View(viewModel);

		}
	}
}
