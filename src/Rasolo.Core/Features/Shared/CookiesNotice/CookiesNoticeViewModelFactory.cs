
using System.Linq;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Settings;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Zone.UmbracoMapper.V8;


namespace Rasolo.Core.Features.Shared.CookiesNotice
{
	public class CookiesNoticeViewModelFactory : ICookiesNoticeViewModelFactory
	{
		public readonly IUmbracoMapper _mapper;
		public readonly IUmbracoContextFactory _umbracoContextFactory;
		public CookiesNoticeViewModelFactory(IUmbracoMapper mapper, IUmbracoContextFactory context)
		{
			this._mapper = mapper;
			this._umbracoContextFactory = context;
		}
		public CookiesNoticeViewModel CreateModel()
		{
			using (var umbracoContextReference = this._umbracoContextFactory.EnsureUmbracoContext())
			{
				var cache = umbracoContextReference.UmbracoContext.Content;
				var globalSettingsContentType = cache.GetContentType("globalSettings");
				var globalSettingsPage = cache.GetByContentType(globalSettingsContentType).FirstOrDefault();
				//var globalSettingsPage = Umbraco.ContentAtRoot().FirstOrDefault(x => x.IsDocumentType("globalSettings"));
				var globalSettingsModel = new GlobalSettingsModel();
				this._mapper.Map(globalSettingsPage, globalSettingsModel);

				var viewModel = new CookiesNoticeViewModel()
				{
					CookieNoticeText = globalSettingsModel.CookieNoticeText,
					CookieLink = globalSettingsModel.CookieLink
				};

				return viewModel;
			}
	

		}
	}
}