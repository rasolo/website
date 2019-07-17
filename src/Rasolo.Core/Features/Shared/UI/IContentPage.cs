using System.Web;

namespace Rasolo.Core.Features.Shared.UI
{
	interface IContentPage
	{
		string Name { get; }
		string Title { get; }
		IHtmlString MainBody { get; }
		string TeaserHeading { get; }
	}
}
