using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPage
	{
		public virtual string Name { get; set; }
		public virtual string Title { get; set; }
		public MediaFile HeroImage { get; set; }
	}
}