using System.Web.Mvc;
using Zone.UmbracoMapper.V8;
using Rasolo.Core.Features.Shared.UI;
using Umbraco.Web.Models;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPageController : BaseContentPageController<BlogPostPage>
	{
		public BlogPostPageController(IUmbracoMapper umbracoMapper, IBaseContentPageViewModelFactory<BlogPostPage> viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
		}

		public override ActionResult Index(ContentModel model)
		{
			var viewModel  = (BlogPostPage)((ViewResult)base.Index(model)).Model;
			viewModel.CreatedDate = model.Content.CreateDate;
			viewModel.PageUrl = model.Content.Url;

			return View(viewModel);
		}
	}
}