using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.Settings
{
	public interface IGlobalSettings
	{
		string CookieNoticeText { get; }
		Link CookieLink { get; }
	}
}