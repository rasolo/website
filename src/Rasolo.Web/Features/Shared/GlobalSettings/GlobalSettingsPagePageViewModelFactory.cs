//using System.Web;
//using Rasolo.Web.Features.Shared.Compositions;
//using Rasolo.Services.Abstractions.UmbracoHelper;
//using Zone.UmbracoMapper.V8;

//namespace Rasolo.Web.Features.Shared.GlobalSettings
//{
//	public class GlobalSettingsPagePageViewModelFactory : IGlobalSettingsPageViewModelFactory
//	{
//		private readonly IUmbracoMapper _mapper;
//		private readonly IUmbracoHelper _umbracoHelper;

//		public GlobalSettingsPagePageViewModelFactory(IUmbracoMapper mapper, IUmbracoHelper umbracoHelper)
//		{
//			this._mapper = mapper;
//			_umbracoHelper = umbracoHelper;
//		}

//		public void SetViewModelProperties(GlobalSettingsPageViewModel viewModel)
//		{
//			viewModel.CookiesNoticeText = !string.IsNullOrEmpty(viewModel.CookiesNoticeText) ? viewModel.CookiesNoticeText : string.Empty;
//			viewModel.CookiesAcceptText = !string.IsNullOrEmpty(viewModel.CookiesAcceptText) ? viewModel.CookiesAcceptText : string.Empty;
//			viewModel.CookiesLink = viewModel.CookiesLink ?? new Umbraco.Web.Models.Link() { Url = "/" };
//			viewModel.HomeText = !string.IsNullOrEmpty(viewModel.HomeText) ? viewModel.HomeText : string.Empty;
//			viewModel.SearchPageUrl = this._umbracoHelper.SearchPage?.Url;
//			var currentPage = new BaseContentPage();
//			this._mapper.Map(this._umbracoHelper.AssignedContentItem, currentPage);
//			viewModel.HomeTextColor = currentPage.HomeTextColor;
//			viewModel.CurrentPageIsStartPage = currentPage.Id == this._umbracoHelper.StartPage?.Id;
//		}


//		public GlobalSettingsPageViewModel CreateModel(HttpCookieCollection httpCookieCollection)
//		{
//			var globalSettingsPage = this._umbracoHelper.GlobalSettingsPage;
//			var viewModel = new GlobalSettingsPageViewModel();
//			if (globalSettingsPage == null)
//			{
//				return new GlobalSettingsPageViewModel();
//			}

//			this._mapper.Map(globalSettingsPage, viewModel);

//			SetViewModelProperties(viewModel);
//			viewModel.ShowCookiesNotice = httpCookieCollection?[Constants.CookiesNotice.CookiesNoticeCookieName] == null;

//			return viewModel;
//		}
//	}
//}