using Anaximapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Rasolo.Web.Features.Feed
{
	public class FeedPageController : RenderController
	{
		private readonly IPublishedContentMapper _anaxiMapper;
		private readonly IFeedPageViewModelFactory<FeedPage> _viewModelFactory;

		public FeedPageController(IPublishedContentMapper anaxiMapper, IFeedPageViewModelFactory<FeedPage> viewModelFactory, ILogger<RenderController>logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
		{
			_anaxiMapper = anaxiMapper;
			_viewModelFactory = viewModelFactory;
		}
		
		public IActionResult FeedPage(ContentModel contentModel)
		{
			var mappedModel = new FeedPage();
			mappedModel.HttpContext = HttpContext;
			this._anaxiMapper.Map(contentModel.Content, mappedModel);
			
			return File(this._viewModelFactory.CreateModel(mappedModel, contentModel).ToArray(), "text/xml; charset=utf-8");
		}
	}
}