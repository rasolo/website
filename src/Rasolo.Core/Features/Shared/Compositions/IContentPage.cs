using System.Web;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public interface IContentPage
	{
		string Name { get; }
		string Title { get; set; }
		IHtmlString MainBody { get; set; }
		MediaFile HeroImage { get; }
		bool ShowHeroImage { get; set; }
	}
}
