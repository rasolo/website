using AutoMapper.Configuration.Annotations;
using System.Collections.Generic;
using Rasolo.Core.Features.Shared.Compositions;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPage : BaseContentPage
	{
		[Ignore]
		public ICollection<BlogPostPage.BlogPostPage> BlogPosts { get; set; }
	}
}