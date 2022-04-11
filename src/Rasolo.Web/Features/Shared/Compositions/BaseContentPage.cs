using Anaximapper.Models;
using Microsoft.AspNetCore.Html;
using NPoco;
using Rasolo.Web.Features.Shared.DocumentTypeInterfaces;
using Rasolo.Web.Features.Shared.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Rasolo.Web.Features.Shared.Compositions
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
		public string UrlSegment { get; set; }
		public string Url { get; set; }
		public virtual string MetaTitle { get; set; }
		public virtual string MetaDescription { get; set; }
		[JsonIgnore]
		public virtual IPublishedContent Parent { get; set; }
		public DateTime UpdateDate { get; set; }
		public DateTime CreateDate { get; set; }

		[Ignore]
		public string PageAndSiteTitle => !string.IsNullOrEmpty(MetaTitle) ? $"{SiteTitle} | {MetaTitle}" :
			!string.IsNullOrEmpty(Title) ? $"{SiteTitle} | {Title}" : $"{SiteTitle} | {Name}";

		public IEnumerable<BreadCrumb> BreadCrumbs { get; set; }
	}
}
