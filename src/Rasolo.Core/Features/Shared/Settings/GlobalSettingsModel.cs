using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.Settings
{
	public class GlobalSettingsModel : IGlobalSettings
	{
		public string CookiesNoticeText { get; set; }
		public string CookiesAcceptText { get; set; }
		public Link CookiesLink { get; set; }
	}
}