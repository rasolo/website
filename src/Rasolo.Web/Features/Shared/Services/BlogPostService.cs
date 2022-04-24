using System.Collections.Generic;
using System.Linq;
using Anaximapper;
using Rasolo.Web.Features.BlogPostPage;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Rasolo.Web.Features.Shared.Services
{
	public class BlogPostService : IBlogPostService
	{
		private readonly IPublishedContentMapper anaxiMapper;
		private readonly IBlogPostPageViewModelFactory _blogPostPageViewModelFactory;

		public BlogPostService(IPublishedContentMapper anaxiMapper, IBlogPostPageViewModelFactory blogPostPageViewModelFactory)
		{
			this.anaxiMapper = anaxiMapper;
			_blogPostPageViewModelFactory = blogPostPageViewModelFactory;
		}

		public IEnumerable<BlogPostPage.BlogPostPage> GetMappedBlogPosts(IEnumerable<IPublishedContent> blogPostPagesAsPublishedContent)
		{
			var blogPostPages = new List<BlogPostPage.BlogPostPage>();
		
			this.anaxiMapper.MapCollection(blogPostPagesAsPublishedContent, blogPostPages);

			foreach (var blogPostPage in blogPostPages)
			{
				blogPostPage.Url = blogPostPage.Parent.Url() + blogPostPage.UrlSegment;
			}
			return blogPostPages.Select(x => this._blogPostPageViewModelFactory.CreateModel(x, null))
				.OrderByDescending(x => x.CreateDate);
		}
	}
}