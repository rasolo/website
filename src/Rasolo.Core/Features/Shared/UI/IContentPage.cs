using System.Web;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.Shared.UI
{
	public interface IContentPage
	{
		string Name { get; }
		string Title { get; set; }
		IHtmlString MainBody { get; set; }
		string TeaserHeading { get; }
		IHtmlString TeaserPreamble { get; }
		MediaFile TeaserMedia { get; }
	}
}
