using System.Web;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.Shared.UI
{
	interface IContentPage
	{
		string Name { get; }
		string Title { get; }
		IHtmlString MainBody { get; }
		string TeaserHeading { get; }
		IHtmlString TeaserPreamble { get; }
		MediaFile TeaserMedia { get; }

	}
}
