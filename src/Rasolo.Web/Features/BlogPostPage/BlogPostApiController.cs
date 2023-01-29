using J2N.Collections.Generic;
using Newtonsoft.Json;
using Rasolo.Services.Constants;
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
        public string GetBlogPosts(int p = 1)
        {
            var blogPosts  = _blogPostService
                .GetMappedBlogPosts(_umbracoService
                    .GetAllPagesByDocumentTypeAtRootLevel(DocumentTypeAlias.BlogPostPage)).Skip(4*p).Take(5).ToList();

            if(blogPosts == null || blogPosts.Count == 0)
            {
                return JsonConvert.SerializeObject(new List<BlogPostJsonDto>());
            }

            var blogPostsJsonDtoList = new List<BlogPostJsonDto>();
            foreach (var blogPost in blogPosts)
            {
                var blogPostJsonDto = new BlogPostJsonDto();
                blogPostJsonDto.ParentName= blogPost.Parent.Name;
                blogPostJsonDto.ParentUrl = blogPost.Parent.Url();
                blogPostJsonDto.Preamble = blogPost.Preamble;
                blogPostJsonDto.ShowTeaserMediaAltText = blogPost.ShowTeaserMediaAltText;
                blogPostJsonDto.TeaserHeading = blogPost.TeaserHeading;
                blogPostJsonDto.TeaserUrl = blogPost.TeaserUrl;
                blogPostJsonDto.BlogPostUrl = blogPost.Url;
                blogPostsJsonDtoList.Add(blogPostJsonDto);
                blogPostJsonDto.CreateDate = blogPost.CreateDate.ToString("yyyy-MM-dd");
			}

            return JsonConvert.SerializeObject(blogPostsJsonDtoList);
        }
    }
}
