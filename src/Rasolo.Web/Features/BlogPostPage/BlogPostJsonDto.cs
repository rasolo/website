using Anaximapper.Attributes;
using Rasolo.Web.Features.Shared.Attributes;
using Rasolo.Web.Features.Shared.Constants.MediaCropAliases;
using Rasolo.Web.Features.Shared.Constants.PropertyTypeAlias;

namespace Rasolo.Web.Features.BlogPostPage
{
	public class BlogPostJsonDto
	{
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
