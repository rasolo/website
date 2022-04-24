using Rasolo.Web.Features.Shared.Extensions;
using Rasolo.Web.Features.Shared.GlobalSettings;
using Umbraco.Cms.Core.Models;

namespace Rasolo.Web.Features.Shared.Constants.PropertyTypeAlias
{
	public class GlobalSettingsPagePropertyAlias : IGlobalSettingsPage
	{
		public static readonly string CookiesNoticeTextAlias = nameof(CookiesNoticeText).FirstLetterToLower();
		public static readonly string CookiesAcceptTextAlias = nameof(CookiesAcceptText).FirstLetterToLower();
		public static readonly string CookiesLinkAlias = nameof(CookiesLink).FirstLetterToLower();
		public static readonly string HomeTextAlias = nameof(HomeText).FirstLetterToLower();
		public string CookiesNoticeText { get; }
		public string CookiesAcceptText { get; }
		public Link CookiesLink { get; }
		public string HomeText { get; }
	}
}