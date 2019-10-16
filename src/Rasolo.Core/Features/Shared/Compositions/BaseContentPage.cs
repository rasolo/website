﻿using System.Web;
using AutoMapper.Configuration.Annotations;
using Rasolo.Core.Features.Shared.DocumentTypeInterfaces;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public class BaseContentPage : IContentPage, IMetaData
	{
		public virtual string Name { get; set; }
		public virtual string Title { get; set; }
		public virtual IHtmlString MainBody { get; set; }
		public MediaFile HeroImage { get; set; }
		[Ignore]
		public bool ShowHeroImage { get; set; }
		public virtual string SiteTitle => Constants.GlobalSettings.SiteTitle;
		public string Url { get; set; }
		public virtual string MetaTitle { get; set; }
		public virtual string MetaDescription { get; set; }

		[Ignore]
		public string PageAndSiteTitle => !string.IsNullOrEmpty(MetaTitle) ? $"{SiteTitle} | {MetaTitle}" :
			!string.IsNullOrEmpty(Title) ? $"{SiteTitle} | {Title}" : $"{SiteTitle} | {Name}";
	}
}