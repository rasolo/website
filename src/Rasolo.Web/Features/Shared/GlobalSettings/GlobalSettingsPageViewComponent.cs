using Anaximapper;
using Microsoft.AspNetCore.Mvc;

namespace Rasolo.Web.Features.Shared.GlobalSettings
{
	public class GlobalSettingsPageViewComponent : ViewComponent
	{
		private readonly IGlobalSettingsPageViewModelFactory _globalSettingsPageViewModelFactory;


		public GlobalSettingsPageViewComponent(IGlobalSettingsPageViewModelFactory globalSettingsPageViewModelFactory)
		{
			_globalSettingsPageViewModelFactory = globalSettingsPageViewModelFactory;
		}

		public IViewComponentResult Invoke()
		{
			var viewModel = this._globalSettingsPageViewModelFactory.CreateModel();

			return View(viewModel);
		}
	}
}