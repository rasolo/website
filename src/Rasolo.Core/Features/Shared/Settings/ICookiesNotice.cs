using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.Settings
{
	public interface ICookiesNotice
	{
		string CookiesNoticeText { get; }
		string CookiesAcceptText { get; }
		Link CookiesLink { get;}
	}
}