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

		public IViewComponentResult Invoke(int id)
		{
			var viewModel = this._globalSettingsPageViewModelFactory.CreateModel(id.ToString());

			return View(viewModel);
		}
	}
}