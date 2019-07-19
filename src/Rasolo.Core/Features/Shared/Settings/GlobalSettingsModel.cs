using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.Settings
{
	public class GlobalSettingsModel : IGlobalSettings
	{
		public string CookieNoticeText { get; set; }

		public Link CookieLink { get; set; }
	}
}