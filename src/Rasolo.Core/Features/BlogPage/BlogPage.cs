using AutoMapper.Configuration.Annotations;
using Rasolo.Core.Features.Shared.UI;
using System.Collections.Generic;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPage : BaseContentPage
	{
		[Ignore]
		public ICollection<BlogPostPage.BlogPostPage> BlogPosts { get; set; }
	}
}