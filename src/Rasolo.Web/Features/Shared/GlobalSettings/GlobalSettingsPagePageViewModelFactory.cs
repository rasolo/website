using Anaximapper;
using Microsoft.AspNetCore.Http;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Rasolo.Web.Features.Shared.Compositions;
using Rasolo.Web.Features.Shared.GlobalSettings;
using Umbraco.Extensions;

namespace Rasolo.Web.Features.Shared
{
	public class GlobalSettingsPagePageViewModelFactory : IGlobalSettingsPageViewModelFactory
	{
		private readonly IPublishedContentMapper anaxiMapper;
		private readonly IUmbracoHelper _umbracoHelper;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public GlobalSettingsPagePageViewModelFactory(IPublishedContentMapper anaxiMapper, IUmbracoHelper umbracoHelper, IHttpContextAccessor httpContextAccessor)
		{
			this.anaxiMapper = anaxiMapper;
			_umbracoHelper = umbracoHelper;
			_httpContextAccessor = httpContextAccessor;
		}

		public void SetViewModelProperties(GlobalSettingsPageViewModel viewModel)
		{
			viewModel.CookiesNoticeText = !string.IsNullOrEmpty(viewModel.CookiesNoticeText) ? viewModel.CookiesNoticeText : string.Empty;
			viewModel.CookiesAcceptText = !string.IsNullOrEmpty(viewModel.CookiesAcceptText) ? viewModel.CookiesAcceptText : string.Empty;
			viewModel.CookiesLink = viewModel.CookiesLink;
			viewModel.HomeText = !string.IsNullOrEmpty(viewModel.HomeText) ? viewModel.HomeText : string.Empty;
			viewModel.SearchPageUrl = this._umbracoHelper.SearchPage?.Url();
			var currentPage = new BaseContentPage();
			// this.anaxiMapper.Map(this._umbracoHelper.AssignedContentItem, currentPage);
			// viewModel.HomeTextColor = currentPage.HomeTextColor;
			// viewModel.CurrentPageIsStartPage = currentPage.Id == this._umbracoHelper.StartPage?.Id;
		}


		public GlobalSettingsPageViewModel CreateModel()
		{
			var globalSettingsPage = this._umbracoHelper.GlobalSettingsPage;
			var viewModel = new GlobalSettingsPageViewModel();
			if (globalSettingsPage == null)
			{
				return new GlobalSettingsPageViewModel();
			}

			this.anaxiMapper.Map(globalSettingsPage, viewModel);

			SetViewModelProperties(viewModel);
			viewModel.ShowCookiesNotice = this._httpContextAccessor.HttpContext.Request.Cookies?[Constants.CookiesNotice.CookiesNoticeCookieName] == null;


			return viewModel;
		}
	}
}