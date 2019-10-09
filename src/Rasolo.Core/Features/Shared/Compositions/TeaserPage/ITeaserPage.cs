using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.Shared.Compositions.TeaserPage
{
	public interface ITeaserPage
	{
		 string TeaserHeading { get; set; }
		 MediaFile TeaserMedia { get; set; }
		 string TeaserPreamble { get; set; }
	}
}
