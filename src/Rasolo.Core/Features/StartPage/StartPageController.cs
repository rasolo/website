using Rasolo.Core.Features.Shared.Controllers;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPageController : BasePageController<StartPage>
	{
		private readonly IStartPageViewModelFactory _startPageViewModelFactory;

		public StartPageController(IUmbracoMapper umbracoMapper, IStartPageViewModelFactory startPageViewModelFactory) : base(umbracoMapper)
		{
			_startPageViewModelFactory = startPageViewModelFactory;
		}


		public override ActionResult Index(ContentModel model)
		{
			var startPageModel = this.MapModel(model.Content);
			startPageModel = _startPageViewModelFactory.CreateModel(startPageModel);
			return View(startPageModel);
		}
	}
}