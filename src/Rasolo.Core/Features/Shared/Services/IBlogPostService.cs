using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.Services
{
	public interface IBlogPostService
	{
		IEnumerable<BlogPostPage.BlogPostPage> GetMappedBlogPosts(IEnumerable<IPublishedContent> blogPostPagesAsPublishedContent);
	}
}