using NPoco;
using Rasolo.Web.Features.Shared.Compositions;
using System.Collections.Generic;

namespace Rasolo.Web.Features.StartPage
{
	public class StartPage : BaseContentPage
	{
		[Ignore]
		public ICollection<BlogPage.BlogPage> BlogPages { get; set; }
		[Ignore]
		public ICollection<BlogPostPage.BlogPostPage> BlogPostPages { get; set; }
	}
}
