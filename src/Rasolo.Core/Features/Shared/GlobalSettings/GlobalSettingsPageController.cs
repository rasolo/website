using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Services;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Zone.UmbracoMapper.V8;

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