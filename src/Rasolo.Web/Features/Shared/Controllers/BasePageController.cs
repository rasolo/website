using Anaximapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;

namespace Rasolo.Web.Features.Shared.Controllers
{
	public abstract class BasePageController<TModel> : RenderController, IBasePageController<TModel> where TModel : class, new()
	{
		private readonly IPublishedContentMapper _anaxiMapper;

		protected BasePageController(IPublishedContentMapper anaxiMapper, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor) 
		{
			this._anaxiMapper = anaxiMapper;
		}

		public virtual IActionResult Index(ContentModel model)
		{
			return CurrentTemplate(model);
			//return View(this.MapModel(model.Content));
		}

		public virtual TModel MapModel(IPublishedContent content)
		{
			var model = new TModel();
			this._anaxiMapper.Map(content, model);
			return model;
		}
	}
}