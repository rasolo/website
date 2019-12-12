using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogsPage
{
	public class BlogsPageController : BaseContentPageController<BlogsPage>
	{
		public BlogsPageController(IUmbracoMapper umbracoMapper,
			IBaseContentPageViewModelFactory<BlogsPage> viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
		}

		public override ActionResult Index(ContentModel model)
		{
			var viewModel = (BlogsPage) ((ViewResult) base.Index(model)).Model;

			return View(viewModel);
		}
	}
}