using System.Web.Mvc;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Umbraco.Web.Mvc;

namespace Rasolo.Core.Features.Shared.CookiesNotice
{
	public class CookiesNoticeController : SurfaceController
	{
		private IGlobalSettingsPageViewModelFactory GlobalSettingsPageViewModelFactory { get; }

		public CookiesNoticeController(IGlobalSettingsPageViewModelFactory globalSettingsPageViewModelFactory)
		{
			this.GlobalSettingsPageViewModelFactory = globalSettingsPageViewModelFactory;
		}

		[ChildActionOnly]
		public ActionResult Index()
		{
			var viewModel = this.GlobalSettingsPageViewModelFactory.CreateModel(Request?.Cookies);

			return PartialView("CookiesNotice", viewModel);
		}
	}
}