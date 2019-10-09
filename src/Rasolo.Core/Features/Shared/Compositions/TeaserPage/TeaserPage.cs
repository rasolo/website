using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.Shared.Compositions.TeaserPage
{
	public class TeaserPage : ITeaserPage
	{
		public string TeaserHeading { get; set; }
		public MediaFile TeaserMedia { get; set; }
		public string TeaserPreamble { get; set; }
	}
}