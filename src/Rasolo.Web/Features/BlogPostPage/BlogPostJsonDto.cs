using Umbraco.Extensions;

namespace Rasolo.Web.Features.BlogPostPage
{
	public class BlogPostJsonDto
	{
		public BlogPostJsonDto(BlogPostPage blogPost)
		{
            ParentName = blogPost.Parent.Name;
            ParentUrl = blogPost.Parent.Url();
            Preamble = blogPost.Preamble;
            ShowTeaserMediaAltText = blogPost.ShowTeaserMediaAltText;
            TeaserHeading = blogPost.TeaserHeading;
            TeaserUrl = blogPost.TeaserUrl;
            BlogPostUrl = blogPost.Url;
            CreateDate = blogPost.CreateDate.ToString("yyyy-MM-dd");
        }
		public string TeaserHeading { get; set; }
		
		public string TeaserUrl { get; set; }

		public string ParentUrl { get; set; }

		public string ParentName { get; set; }
		public string TeaserMediaAltText { get; set; }
		public bool ShowTeaserMediaAltText { get; set; }
		public string Preamble { get; set; }
		public string BlogPostUrl { get; set; }
		public string CreateDate { get; internal set; }
	}
}
