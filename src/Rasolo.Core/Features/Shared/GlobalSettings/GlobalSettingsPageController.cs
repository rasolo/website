using System.Web.Mvc;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Umbraco.Web.Mvc;

namespace Rasolo.Core.Features.Shared.GlobalSettings
{
	public class GlobalSettingsPageController : SurfaceController
	{
		private readonly IGlobalSettingsPageViewModelFactory _globalSettingsPageViewModelFactory;
		private readonly IUmbracoHelper _umbracoHelper;

		public GlobalSettingsPageController(IGlobalSettingsPageViewModelFactory globalSettingsPageViewModelFactory, IUmbracoHelper umbracoHelper)
		{
			this._globalSettingsPageViewModelFactory = globalSettingsPageViewModelFactory;
			_umbracoHelper = umbracoHelper;
		}

		[ChildActionOnly]
		public ActionResult Index()
		{
			var viewModel = this._globalSettingsPageViewModelFactory.CreateModel(null);

			return PartialView("GlobalSettings", viewModel);
		}
	}
}