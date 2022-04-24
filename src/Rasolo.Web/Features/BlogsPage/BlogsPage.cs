using System.Collections.Generic;
using Rasolo.Web.Features.Shared.Compositions;
using Rasolo.Web.Features.Shared.DocumentTypeInterfaces;

namespace Rasolo.Web.Features.BlogsPage
{
	public class BlogsPage : BaseContentPage, IBlogTeaser
	{
		public IEnumerable<BlogPage.BlogPage> BlogPages { get; set; }

		public bool ShowPosts { get; set; }
		public IEnumerable<ITeaserPage> Posts { get; set; }
		public string PostsTitle { get; set; }
	}
}