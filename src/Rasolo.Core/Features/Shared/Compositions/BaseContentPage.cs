using Anaximapper.Models;
using Microsoft.AspNetCore.Html;
using NPoco;
using Rasolo.Core.Features.Shared.DocumentTypeInterfaces;
using Rasolo.Core.Features.Shared.Models;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public class BaseContentPage : IContentPage, IMetaData
	{
		public int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Title { get; set; }
		public virtual HtmlString MainBody { get; set; }
		public MediaFile HeroImage { get; set; }
		public string HomeTextColor { get; set; }
		[Ignore]
		public bool ShowHeroImage { get; set; }
		public virtual IEnumerable<IPublishedContent> Children { get; set; }
		public virtual string SiteTitle => "Rasolo";
		public string Url { get; set; }
		public virtual string MetaTitle { get; set; }
		public virtual string MetaDescription { get; set; }
		public virtual IPublishedContent Parent { get; set; }
		public DateTime UpdateDate { get; set; }
		public DateTime CreateDate { get; set; }

		[Ignore]
		public string PageAndSiteTitle => !string.IsNullOrEmpty(MetaTitle) ? $"{SiteTitle} | {MetaTitle}" :
			!string.IsNullOrEmpty(Title) ? $"{SiteTitle} | {Title}" : $"{SiteTitle} | {Name}";

		public IEnumerable<BreadCrumb> BreadCrumbs { get; set; }
	}
}
