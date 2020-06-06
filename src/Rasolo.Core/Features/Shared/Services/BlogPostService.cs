using System.Collections.Generic;
using System.Linq;
using Rasolo.Core.Features.BlogPostPage;
using Umbraco.Core.Models.PublishedContent;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.Services
{
	public class BlogPostService : IBlogPostService
	{
		private readonly IBlogPostPageViewModelFactory _blogPostPageViewModelFactory;
		public IUmbracoMapper UmbracoMapper { get; }

		public BlogPostService(IUmbracoMapper umbracoMapper, IBlogPostPageViewModelFactory blogPostPageViewModelFactory)
		{
			_blogPostPageViewModelFactory = blogPostPageViewModelFactory;
			UmbracoMapper = umbracoMapper;
		}

		public IEnumerable<BlogPostPage.BlogPostPage> GetMappedBlogPosts(IEnumerable<IPublishedContent> blogPostPagesAsPublishedContent)
		{
			var blogPostPages = new List<BlogPostPage.BlogPostPage>();
		
			this.UmbracoMapper.MapCollection(blogPostPagesAsPublishedContent, blogPostPages);
			return blogPostPages.Select(x => this._blogPostPageViewModelFactory.CreateModel(x, null))
				.OrderByDescending(x => x.CreateDate);
		}
	}
}