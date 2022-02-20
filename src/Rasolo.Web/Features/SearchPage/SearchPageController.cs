using Anaximapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Rasolo.Web.Features.Shared.Compositions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Rasolo.Web.Features.SearchPage
{
	public class SearchPageController : BaseContentPageController<Web.Features.SearchPage.SearchPage>
	{
		private readonly ISearchPageViewModelFactory _viewModelFactory;

		public SearchPageController(IPublishedContentMapper anaxiMapper, ISearchPageViewModelFactory viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, viewModelFactory, logger, compositeViewEngine, umbracoContextAccessor)

		{
			_viewModelFactory = viewModelFactory;
		}
		
		public IActionResult SearchPage(ContentModel contentModel)
		{
			var mappedModel = this.MapModel(contentModel.Content);
			var viewModel = this._viewModelFactory.CreateModel(mappedModel, contentModel);

			return View(viewModel);
			//return View($"../{contentModel.Content.ContentType.Alias}/Index", viewModel);

		}
	}
}