using System.Web.Mvc;
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
			var viewModel = _cookiesNoticeViewModelFactory.CreateModel(Request?.Cookies);

			return PartialView("CookieNotice", viewModel);
		}
	}
}