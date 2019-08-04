using System.Web;

namespace Rasolo.Core.Features.Shared.CookiesNotice
{
	public interface ICookiesNoticeViewModelFactory
	{
		CookiesNoticeViewModel CreateModel(HttpCookieCollection httpCookieCollection);
	}
}