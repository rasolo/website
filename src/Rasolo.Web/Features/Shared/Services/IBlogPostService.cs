using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Rasolo.Web.Features.Shared.Services
{
	public interface IBlogPostService
	{
		IEnumerable<BlogPostPage.BlogPostPage> GetMappedBlogPosts(IEnumerable<IPublishedContent> blogPostPagesAsPublishedContent);
	}
}