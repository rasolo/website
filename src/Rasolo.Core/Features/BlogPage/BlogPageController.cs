using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPageController : BaseContentPageController<BlogPage>
	{
		private readonly IBlogPageViewModelFactory _viewModelFactory;

		public BlogPageController(IUmbracoMapper umbracoMapper, IBlogPageViewModelFactory viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
			_viewModelFactory = viewModelFactory;
		}

		public new ActionResult Index(ContentModel model)
		{
			var blogPage = this._viewModelFactory.CreateModel(model);
			

			return View(blogPage);
		}
	}
}