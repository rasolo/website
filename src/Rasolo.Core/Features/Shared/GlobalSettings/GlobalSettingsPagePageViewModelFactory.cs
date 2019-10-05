using System.Web;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Services;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.GlobalSettings
{
	public class GlobalSettingsPagePageViewModelFactory : IGlobalSettingsPageViewModelFactory
	{
		private readonly IUmbracoMapper _mapper;
		private readonly IUmbracoService _umbracoService;

		public GlobalSettingsPagePageViewModelFactory(IUmbracoMapper mapper, IUmbracoService umbracoService)
		{
			this._mapper = mapper;
			this._umbracoService = umbracoService;
		}

		public GlobalSettingsPageViewModel CreateModel(HttpCookieCollection httpCookieCollection)
		{
			var globalSettingsPage = _umbracoService.GetFirstPageByDocumentTypeAtRootLevel(DocumentTypeAlias.GlobalSettingsPage);
			var viewModel = new GlobalSettingsPageViewModel();
			if (globalSettingsPage == null)
			{
				return new GlobalSettingsPageViewModel();
			}

			this._mapper.Map(globalSettingsPage, viewModel);


			viewModel.CookiesNoticeText = !string.IsNullOrEmpty(viewModel.CookiesNoticeText) ? viewModel.CookiesNoticeText : string.Empty;
			viewModel.CookiesAcceptText = !string.IsNullOrEmpty(viewModel.CookiesAcceptText) ? viewModel.CookiesAcceptText : string.Empty;
			viewModel.CookiesLink = viewModel.CookiesLink ?? new Umbraco.Web.Models.Link() { Url = "/" };
			viewModel.HomeText = !string.IsNullOrEmpty(viewModel.HomeText) ? viewModel.HomeText : string.Empty;
			viewModel.ShowCookiesNotice = httpCookieCollection?[Constants.CookiesNotice.CookiesNoticeCookieName] == null;

			return viewModel;

		}
	}
}