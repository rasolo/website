using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Rasolo.Core.Features.Shared.Services
{
	public interface IBlogPostService
	{
		IEnumerable<BlogPostPage.BlogPostPage> GetMappedBlogPosts(IEnumerable<IPublishedContent> blogPostPagesAsPublishedContent);
	}
}