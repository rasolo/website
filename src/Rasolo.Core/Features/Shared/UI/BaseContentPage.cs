using System.Web;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.Shared.UI
{
	public class BaseContentPage : IContentPage
	{
		public virtual string Name { get; set; }
		public virtual string Title { get; set; }
		public virtual IHtmlString MainBody { get; set; }
		public virtual string TeaserHeading { get; set; }
		public virtual IHtmlString TeaserPreamble { get; set; }
		public MediaFile TeaserMedia { get; set; }
		public MediaFile HeroImage { get; set; }
		public bool ShowHeroImage { get; set; }
	}
}