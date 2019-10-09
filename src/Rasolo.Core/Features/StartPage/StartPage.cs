using System.Collections.Generic;
using AutoMapper.Configuration.Annotations;
using Rasolo.Core.Features.Shared.Compositions;

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