using System.Collections.Generic;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.DocumentTypeInterfaces;

namespace Rasolo.Core.Features.BlogsPage
{
	public class BlogsPage : BaseContentPage, IBlogTeaser
	{
		public IEnumerable<BlogPage.BlogPage> BlogPages { get; set; }

		public bool ShowPosts { get; set; }
		public IEnumerable<ITeaserPage> Posts { get; set; }
		public string PostsTitle { get; set; }
	}
}