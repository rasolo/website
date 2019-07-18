using Rasolo.Core.Features.Shared.UI;
using System.Collections.Generic;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPage : BaseContentPage
	{
		public MediaFile HeroImage { get; set; }
		public IEnumerable<BlogPostPage.BlogPostPage> BlogPostPages { get; set; }
	}
}