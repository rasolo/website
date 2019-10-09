using System.Web;
using AutoMapper.Configuration.Annotations;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public class BaseContentPage : IContentPage
	{
		public virtual string Name { get; set; }
		public virtual string Title { get; set; }
		public virtual IHtmlString MainBody { get; set; }
		public MediaFile HeroImage { get; set; }
		[Ignore]
		public bool ShowHeroImage { get; set; }
	}
}