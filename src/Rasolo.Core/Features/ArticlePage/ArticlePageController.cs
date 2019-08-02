using Rasolo.Core.Features.Shared.Controllers;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.ArticlePage
{
	public class ArticlePageController : BasePageController<ArticlePage>
	{
		public ArticlePageController(IUmbracoMapper umbracoMapper) : base(umbracoMapper)
		{

		}

		public override ActionResult Index(ContentModel model)
		{
			return base.Index(model);
		}
	}
}