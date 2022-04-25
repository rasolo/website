//using System.Web.Mvc;
//using Rasolo.Web.Features.Shared.GlobalSettings;

//namespace Rasolo.Web.Features.Shared.CookiesNotice
//{
//	public class CookiesNoticeController : SurfaceController
//	{
//		private IGlobalSettingsPageViewModelFactory GlobalSettingsPageViewModelFactory { get; }

//		public CookiesNoticeController(IGlobalSettingsPageViewModelFactory globalSettingsPageViewModelFactory)
//		{
//			this.GlobalSettingsPageViewModelFactory = globalSettingsPageViewModelFactory;
//		}

//		[ChildActionOnly]
//		public ActionResult Index()
//		{
//			var viewModel = this.GlobalSettingsPageViewModelFactory.CreateModel(Request?.Cookies);

//			return PartialView("CookiesNotice", viewModel);
//		}
//	}
//}