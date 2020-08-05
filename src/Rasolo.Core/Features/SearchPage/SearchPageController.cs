using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchPageController : BaseContentPageController<SearchPage>
	{
		private readonly ISearchPageViewModelFactory _viewModelFactory;

		public SearchPageController(IUmbracoMapper umbracoMapper, ISearchPageViewModelFactory viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
			_viewModelFactory = viewModelFactory;
		}

		[ValidateInput(false)]
		public override ActionResult Index(ContentModel contentModel)
		{
			var mappedModel = this.MapModel(contentModel.Content);
			var viewModel = this._viewModelFactory.CreateModel(mappedModel, contentModel);

			return View($"../{contentModel.Content.ContentType.Alias}/Index", viewModel);
		}
	}
}