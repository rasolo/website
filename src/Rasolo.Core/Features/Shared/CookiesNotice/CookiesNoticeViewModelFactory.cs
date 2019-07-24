using System.Web;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Core.Features.Shared.Settings;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.CookiesNotice
{
	public class CookiesNoticeViewModelFactory : ICookiesNoticeViewModelFactory
	{
		public readonly IUmbracoMapper _mapper;
		public readonly IUmbracoService _umbracoService;

		public CookiesNoticeViewModelFactory(IUmbracoMapper mapper, IUmbracoService umbracoService)
		{
			this._mapper = mapper;
			this._umbracoService = umbracoService;
		}

		public CookiesNoticeViewModel CreateModel(HttpCookieCollection httpCookieCollection)
		{
			var globalSettingsPage = _umbracoService.GetFirstContentTypeAtRoot(DocumentTypeAlias.GlobalSettingsPage);
			if (globalSettingsPage == null)
			{
				return new CookiesNoticeViewModel();
			}


			var globalSettingsModel = new GlobalSettingsModel();
			this._mapper.Map(globalSettingsPage, globalSettingsModel);

			var viewModel = new CookiesNoticeViewModel()
			{
				CookieNoticeText = !string.IsNullOrEmpty(globalSettingsModel.CookieNoticeText) ? globalSettingsModel.CookieNoticeText : string.Empty,
				CookieLink = globalSettingsModel.CookieLink
			};
			
			if (httpCookieCollection?[Constants.CookiesNotice.CookiesNoticeCookieName] == null)
			{
				viewModel.ShowCookiesNotice = true;
			}

			return viewModel;
		}
	}
}