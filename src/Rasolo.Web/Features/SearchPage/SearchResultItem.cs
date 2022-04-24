using Anaximapper.Attributes;
using Rasolo.Web.Features.Shared.Attributes;
using Rasolo.Web.Features.Shared.Constants.MediaCropAliases;
using Rasolo.Web.Features.Shared.Constants.PropertyTypeAlias;

namespace Rasolo.Web.Features.SearchPage
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