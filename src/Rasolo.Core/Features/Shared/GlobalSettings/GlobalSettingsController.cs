using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Core.Features.Shared.Settings;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.GlobalSettings
{
	public class GlobalSettingsController : SurfaceController
	{
		private readonly IUmbracoMapper _mapper;
		private readonly IGlobalSettingsViewModelFactory _globalSettingsViewModelFactory;
		private readonly IUmbracoService _umbracoService;

		public GlobalSettingsController(IUmbracoMapper mapper, IGlobalSettingsViewModelFactory globalSettingsViewModelFactory, IUmbracoService umbracoService)
		{
			this._mapper = mapper;
			this._globalSettingsViewModelFactory = globalSettingsViewModelFactory;
			this._umbracoService = umbracoService;
		}

		[ChildActionOnly]
		public ActionResult Index()
		{
			var globalSettingsPage = _umbracoService.GetFirstContentTypeAtRoot(DocumentTypeAlias.GlobalSettingsPage);
			if (globalSettingsPage == null)
			{
				return PartialView(new GlobalSettingsViewModel());
			}


			var globalSettingsModel = new GlobalSettingsViewModel();
			this._mapper.Map(globalSettingsPage, globalSettingsModel);
			var viewModel = this._globalSettingsViewModelFactory.CreateModel(globalSettingsModel);

			return PartialView("GlobalSettings", viewModel);
		}
	}
}