using Rasolo.Core.Features.Shared.Extensions;
using Rasolo.Core.Features.Shared.Settings;
using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias
{
	public class GlobalSettingsPagePropertyAlias : ICookiesNotice
	{
		public static readonly string CookieNoticeText = nameof(CookiesNoticeText).FirstLetterToLower();
		public static readonly string CookieLink = nameof(CookiesLink).FirstLetterToLower();
		public string CookiesNoticeText { get; }
		public Link CookiesLink { get; }
	}
}