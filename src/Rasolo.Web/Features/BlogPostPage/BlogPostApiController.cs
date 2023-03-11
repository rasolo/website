using J2N.Collections.Generic;
using Newtonsoft.Json;
using Rasolo.Services.Constants;
using Rasolo.Web.Features.Shared.Extensions;
using Rasolo.Web.Features.Shared.Services;
using System.Linq;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Rasolo.Web.Features.BlogPostPage
{
    public class BlogPostApiController : UmbracoApiController
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IUmbracoService _umbracoService;

        public BlogPostApiController(IBlogPostService blogPostService, IUmbracoService umbracoService)
        {
            _blogPostService = blogPostService;
            _umbracoService = umbracoService;
        }
		public string GetBlogPosts(int p = 1, bool all = false)
		{
			var blogPosts = _blogPostService
				.GetMappedBlogPosts(_umbracoService.GetAllPagesByDocumentTypeAtRootLevel(DocumentTypeAlias.BlogPostPage))
				.Skip(4 * p)
				.Take(all ? int.MaxValue : 5)
				.Select(x => new BlogPostJsonDto(x))
				.ToList();

			if (!blogPosts.Any())
			{
				return JsonConvert.SerializeObject(new List<BlogPostJsonDto>());
			}

			return JsonConvert.SerializeObject(blogPosts);
		}
	}
}
