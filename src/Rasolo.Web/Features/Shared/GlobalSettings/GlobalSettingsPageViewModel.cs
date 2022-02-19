using NPoco;
using Rasolo.Web.Features.Shared.GlobalSettings;
using Umbraco.Cms.Core.Models;

namespace Rasolo.Web.Features.Shared.GlobalSettings
{
	public class GlobalSettingsPageViewModel : IGlobalSettingsPageViewModel
	{
		public string CookiesNoticeText { get; set; }
		public string CookiesAcceptText { get; set; }
		public Link CookiesLink { get; set; }
		[Ignore]
		public bool ShowCookiesNotice { get; set; }
		public string HomeText { get; set; }
		[Ignore]
		public string HomeTextColor { get; set; }

		public int SearchResultsPerPage { get; set; }
		[Ignore]
		public string SearchPageUrl { get; set; }
		[Ignore]
		public bool CurrentPageIsStartPage { get; set; }
	}
}