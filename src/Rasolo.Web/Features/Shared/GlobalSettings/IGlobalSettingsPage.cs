using Umbraco.Cms.Core.Models;

namespace Rasolo.Web.Features.Shared.GlobalSettings
{
	public interface IGlobalSettingsPage
	{
		string CookiesNoticeText { get; }
		string CookiesAcceptText { get; }
		Link CookiesLink { get; }
		string HomeText { get; }

	}
}
