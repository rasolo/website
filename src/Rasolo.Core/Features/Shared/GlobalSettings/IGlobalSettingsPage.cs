using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.GlobalSettings
{
	public interface IGlobalSettingsPage
	{
		string CookiesNoticeText { get; }
		string CookiesAcceptText { get; }
		Link CookiesLink { get; }
		string HomeText { get; }

	}
}
