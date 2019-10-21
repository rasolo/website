using System.Collections.Generic;
using System.Linq;
using Rasolo.Core.Features.Shared.Constants.MediaCropAliases.BlogPostPage;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.Services
{
	public class BlogPostService : IBlogPostService
	{
		public IUmbracoMapper UmbracoMapper { get; }

		public BlogPostService(IUmbracoMapper umbracoMapper)
		{
			UmbracoMapper = umbracoMapper;
		}

		public IEnumerable<BlogPostPage.BlogPostPage> GetMappedBlogPosts(IEnumerable<IPublishedContent> blogPostPagesAsPublishedContent)
		{
			var blogPostPages = new List<BlogPostPage.BlogPostPage>();

			this.UmbracoMapper.MapCollection(blogPostPagesAsPublishedContent, blogPostPages);
			blogPostPages = blogPostPages.Select(x =>
			{
				x.TeaserHeading = !string.IsNullOrEmpty(x.TeaserHeading) ? x.TeaserHeading : !string.IsNullOrEmpty(x.Title) ? x.Title : x.Name;
				return x;
			}).OrderByDescending(x => x.CreateDate).ToList();

			return blogPostPages;
		}
	}
}