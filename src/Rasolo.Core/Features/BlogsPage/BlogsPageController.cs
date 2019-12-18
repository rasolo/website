using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogsPage
{
	public class BlogsPageController : BaseContentPageController<BlogsPage>
	{
		private readonly IBlogsPageViewModelFactory _viewModelFactory;

		public BlogsPageController(IUmbracoMapper umbracoMapper,
			IBlogsPageViewModelFactory viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
			_viewModelFactory = viewModelFactory;
		}

		public override ActionResult Index(ContentModel model)
		{
			var viewModel = (BlogsPage) ((ViewResult) base.Index(model)).Model;
			viewModel = this._viewModelFactory.CreateModel(model);

			return View(viewModel);
		}
	}
}