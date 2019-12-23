using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Controllers;
using Umbraco.Web.Models;
using System.Web;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public class BaseContentPageController<TModel> : BasePageController<TModel> where TModel : class, new()
	{
		private readonly IBaseContentPageViewModelFactory<TModel> _viewModelFactory;

		public BaseContentPageController(Zone.UmbracoMapper.V8.IUmbracoMapper umbracoMapper, IBaseContentPageViewModelFactory<TModel> viewModelFactory) : base(umbracoMapper)
		{
			this._viewModelFactory = viewModelFactory;
		}

		public override ActionResult Index(ContentModel contentModel)
		{
			var mappedModel = this.MapModel(contentModel.Content);
			var viewModel = this._viewModelFactory.CreateModel(mappedModel, contentModel);

			return View(viewModel);

		}
	}
}