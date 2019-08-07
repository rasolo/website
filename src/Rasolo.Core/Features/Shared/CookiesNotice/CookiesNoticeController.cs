using System.Web.Mvc;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Umbraco.Web.Mvc;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.CookiesNotice
{
	public class CookiesNoticeController : SurfaceController
	{
		public IUmbracoMapper _mapper;
		public IGlobalSettingsPageViewModelFactory GlobalSettingsPageViewModelFactory { get; }

		public CookiesNoticeController(IUmbracoMapper mapper, IGlobalSettingsPageViewModelFactory globalSettingsPageViewModelFactory)
		{
			this._mapper = mapper;
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