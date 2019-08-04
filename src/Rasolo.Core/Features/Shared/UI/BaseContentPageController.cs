using Rasolo.Core.Features.ArticlePage;
using Rasolo.Core.Features.Shared.Controllers;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.UI
{
	public class BaseContentPageController<TModel> : BasePageController<TModel> where TModel : class, new()
	{
		public readonly IBaseContentPageViewModelFactory<TModel> _viewModelFactory;

		public BaseContentPageController(Zone.UmbracoMapper.V8.IUmbracoMapper umbracoMapper, IBaseContentPageViewModelFactory<TModel> viewModelFactory) : base(umbracoMapper)
		{
			this._viewModelFactory = viewModelFactory;
		}

		public override ActionResult Index(ContentModel model)
		{
			var mappedModel = this.MapModel(model.Content);
			var viewModel = this._viewModelFactory.CreateModel(mappedModel);

			return View(viewModel);
		}
	}
}