using Rasolo.Web.Features.Shared.Controllers;
using System.Web;
using Anaximapper;
using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Rasolo.Web.Features.BlogPage;

namespace Rasolo.Web.Features.Shared.Compositions
{
	public class BaseContentPageController<TModel> : BasePageController<TModel> where TModel : class, new()
	{
		private readonly IBaseContentPageViewModelFactory<TModel> _viewModelFactory;
		private readonly IPublishedContentMapper anaxiMapper;

		public BaseContentPageController(IPublishedContentMapper anaxiMapper, IBaseContentPageViewModelFactory<TModel> viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, logger, compositeViewEngine, umbracoContextAccessor)
		{
			this.anaxiMapper = anaxiMapper;
			this._viewModelFactory = viewModelFactory;
		}

		public override IActionResult Index(ContentModel contentModel)
		{
			var mappedModel = this.MapModel(contentModel.Content);
			var viewModel = this._viewModelFactory.CreateModel(mappedModel, contentModel);

			return View(viewModel);

		}
	}
}