using System.Linq;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Settings;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.CookiesNotice
{
	public class CookiesNoticeController : SurfaceController
	{
		public IUmbracoMapper _mapper;
		public ICookiesNoticeViewModelFactory _cookiesNoticeViewModelFactory { get; }


		public CookiesNoticeController(IUmbracoMapper mapper, ICookiesNoticeViewModelFactory cookiesNoticeViewModelFactory)
		{
			this._mapper = mapper;
			_cookiesNoticeViewModelFactory = cookiesNoticeViewModelFactory;
		}


		[ChildActionOnly]
		public ActionResult Index()
		{
			//var globalSettingsPage = Umbraco.ContentAtRoot().FirstOrDefault(x => x.IsDocumentType("globalSettings"));
			//var globalSettingsModel = new GlobalSettingsModel();
			//this._mapper.Map(globalSettingsPage, globalSettingsModel);

			//var viewModel = new CookiesNoticeViewModel()
			//{
			//	CookieNoticeText = globalSettingsModel.CookieNoticeText,
			//	CookieLink = globalSettingsModel.CookieLink
			//};
			var viewModel = _cookiesNoticeViewModelFactory.CreateModel();

			return PartialView("CookieNotice", viewModel);
		}
	}
}