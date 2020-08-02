using Rasolo.Core.Features.Shared.Attributes;
using Rasolo.Core.Features.Shared.Constants.MediaCropAliases;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Zone.UmbracoMapper.Common.Attributes;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchResultItem
	{
		public string Title { get; set; }
		public string Url { get; set; }
		public string Preamble { get; set; }
		[PropertyMapping(SourceProperty = BlogPostPagePropertyAlias.TeaserMedia)]
		[MapFromImageCropper(CropName = MediaCropAlias.StartPage)]
		public string TeaserMediaUrl { get; set; }
		public string TeaserMediaAltText { get; set; }
		public bool ShowTeaserMediaAltText { get; set; }
	}
}