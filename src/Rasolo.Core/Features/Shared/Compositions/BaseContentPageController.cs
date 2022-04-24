using Rasolo.Core.Features.Shared.Controllers;
using System.Web;
using Anaximapper;
using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Rasolo.Core.Features.BlogPage;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public class BaseContentPageController<TModel> : BasePageController<TModel> where TModel : class, new()
	{
		private readonly IBaseContentPageViewModelFactory<TModel> _viewModelFactory;
		private object umbracoMapper;
		private IBlogPageViewModelFactory viewModelFactory;

		public BaseContentPageController(object umbracoMapper, IBlogPageViewModelFactory viewModelFactory)
		{
			this.umbracoMapper = umbracoMapper;
			this.viewModelFactory = viewModelFactory;
		}

		public BaseContentPageController(IPublishedContentMapper anaxiMapper, IBaseContentPageViewModelFactory<TModel> viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, logger, compositeViewEngine, umbracoContextAccessor)
		{
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