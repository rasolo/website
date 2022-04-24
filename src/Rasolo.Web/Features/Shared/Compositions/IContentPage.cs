using Anaximapper.Models;
using Microsoft.AspNetCore.Html;

namespace Rasolo.Web.Features.Shared.Compositions
{
	public interface IContentPage
{
		string Name { get; }
		string Title { get; set; }
		HtmlString MainBody { get; set; }
		MediaFile HeroImage { get; }
		bool ShowHeroImage { get; set; }
	}
}
