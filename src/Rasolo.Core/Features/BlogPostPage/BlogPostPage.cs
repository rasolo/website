using System;
using AutoMapper.Configuration.Annotations;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Compositions.TeaserPage;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPage : BaseContentPage, ITeaserPage
	{
		public virtual string Preamble { get; set; }
		[Ignore]
		public DateTime CreatedDate { get; set; }
		[Ignore]
		public string PageUrl { get; set; }

		public string TeaserHeading { get; set; }
		public MediaFile TeaserMedia { get; set; }
		public string TeaserPreamble { get; set; }
	}
}