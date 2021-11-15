using NPoco;
using Rasolo.Core.Features.Shared.Compositions;
using System.Collections.Generic;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPage : BaseContentPage
	{
		[Ignore]
		public ICollection<BlogPage.BlogPage> BlogPages { get; set; }
		[Ignore]
		public ICollection<BlogPostPage.BlogPostPage> BlogPostPages { get; set; }
	}
}
