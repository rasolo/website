using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchResultItem
	{
		public MediaFile TeaserMedia { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public string TeaserUrl { get; set; }
	}
}