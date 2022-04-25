using System.Collections.Generic;
using Anaximapper.Attributes;
using NPoco;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants.MediaCropAliases;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.DocumentTypeInterfaces;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPage : BaseContentPage, IBlogTeaser, ITeaserPage
	{
		[Ignore]
		public ICollection<BlogPostPage.BlogPostPage> BlogPosts { get; set; }

		public IEnumerable<ITeaserPage> Posts { get; set; }
		public string PostsTitle { get; set; }
		public bool ShowPosts { get; set; }
		public string TeaserHeading { get; set; }
		[PropertyMapping(SourceProperty = BlogPagePropertyAlias.TeaserMedia)]
		//[MapFromImageCropper(CropName = MediaCropAlias.StartPage)]
		public string TeaserUrl { get; set; }
		public string ParentUrl { get; set; }
		public string ParentName { get; set; }
	}
}