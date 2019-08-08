using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Rasolo.Core.Features.Shared.GlobalSettings
{
	public class GlobalSettingsPageController : SurfaceController
	{
		private readonly IGlobalSettingsPageViewModelFactory _globalSettingsPageViewModelFactory;

		public GlobalSettingsPageController(IGlobalSettingsPageViewModelFactory globalSettingsPageViewModelFactory)
		{
			this._globalSettingsPageViewModelFactory = globalSettingsPageViewModelFactory;
		}

		[ChildActionOnly]
		public ActionResult Index()
		{
			var viewModel = this._globalSettingsPageViewModelFactory.CreateModel(null);

			return PartialView("GlobalSettings", viewModel);
		}
	}
}