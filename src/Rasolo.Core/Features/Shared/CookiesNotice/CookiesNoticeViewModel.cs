using Rasolo.Core.Features.Shared.Settings;
using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.CookiesNotice
{
	public class CookiesNoticeViewModel : ICookiesNotice
	{
		public string CookiesNoticeText { get; set; }
		public Link CookiesLink { get; set; }
		public bool ShowCookiesNotice { get; set; }
	}
}