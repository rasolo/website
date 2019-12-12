using System.Collections.Generic;
using Rasolo.Core.Features.Shared.Attributes;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants.MediaCropAliases.BlogPostPage;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.DocumentTypeInterfaces;
using Zone.UmbracoMapper.Common.Attributes;

namespace Rasolo.Core.Features.BlogsPage
{
	public class BlogsPage : BaseContentPage, ITeaserPage
	{
		public IEnumerable<BlogPage.BlogPage> BlogPages { get; set; }
		public bool ShowBlogPages { get; set; }
		public string TeaserHeading { get; set; }
		[PropertyMapping(SourceProperty = BlogsPagePropertyAlias.TeaserMedia)]
		[MapFromImageCropper(CropName = BlogsPageMediaCropAliases.Teaser)]
		public string TeaserUrl { get; set; }
	}
}