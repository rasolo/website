using System;
using AutoMapper.Configuration.Annotations;
using Rasolo.Core.Features.Shared.Attributes;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants.MediaCropAliases.BlogPostPage;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Zone.UmbracoMapper.Common.Attributes;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPage : BaseContentPage
	{
		public string TeaserHeading { get; set; }
		public string TeaserMedia { get; set; }
		[PropertyMapping(SourceProperty = BlogPostPagePropertyAlias.TeaserMedia)]
		[MapFromImageCropper(CropName = BlogPostPageMediaCropAliases.StartPage)]
		public string TeaserMediaUrl { get; set; }
		public string TeaserPreamble { get; set; }
	}
}