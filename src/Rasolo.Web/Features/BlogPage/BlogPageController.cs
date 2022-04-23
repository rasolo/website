using Anaximapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Rasolo.Web.Features.Shared.Compositions;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Rasolo.Web.Features.BlogPage
{
	public class BlogPageController : BaseContentPageController<BlogPage>
	{
		private readonly IBlogPageViewModelFactory _viewModelFactory;

		public BlogPageController(IPublishedContentMapper anaxiMapper, IBlogPageViewModelFactory viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, viewModelFactory, logger, compositeViewEngine, umbracoContextAccessor)
		{
			_viewModelFactory = viewModelFactory;
		}

		public IActionResult BlogPage(ContentModel contentModel)
		{
			var mappedModel = this.MapModel(contentModel.Content);
			var viewModel = this._viewModelFactory.CreateModel(mappedModel, contentModel);

			return View(viewModel);
		}
	}
}