using System.Collections.Generic;
using AutoMapper.Configuration.Annotations;
using Rasolo.Core.Features.Shared.Attributes;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants.MediaCropAliases;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.DocumentTypeInterfaces;
using Rasolo.Infrastructure.Models;
using Zone.UmbracoMapper.Common.Attributes;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPage : BaseContentPage, ITeaserPage
	{
		public string TeaserHeading { get; set; }
		[PropertyMapping(SourceProperty = BlogPostPagePropertyAlias.TeaserMedia)]
		[MapFromImageCropper(CropName = MediaCropAlias.StartPage)]
		public string TeaserUrl { get; set; }

		public string ParentUrl { get; set; }

		public string ParentName { get; set; }
		[Ignore] public IEnumerable<CommentViewModel> Comments { get; set; }
		public string TeaserMediaAltText { get; set; }
		public bool ShowTeaserMediaAltText { get; set; }
	}
}