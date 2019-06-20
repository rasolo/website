using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.Controllers
{
	public abstract class BasePageController<TModel> : RenderMvcController where TModel : class, new()
	{
		private readonly IUmbracoMapper _umbracoMapper;

		protected BasePageController(IUmbracoMapper umbracoMapper)
		{
			this._umbracoMapper = umbracoMapper;
		}

		public override ActionResult Index(ContentModel model)
		{
			return View(this.MapModel(model.Content));
		}

		public virtual TModel MapModel(IPublishedContent content)
		{
			var model = new TModel();
			this._umbracoMapper.Map(content, model);
			return model;
		}
	}
}