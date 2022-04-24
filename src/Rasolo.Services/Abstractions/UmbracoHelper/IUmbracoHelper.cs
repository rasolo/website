﻿using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Rasolo.Services.Abstractions.UmbracoHelper
{
	public interface IUmbracoHelper
	{
		IEnumerable<IPublishedContent> AncestorsOrSelf(IPublishedContent content);
		IPublishedContent AssignedContentItem { get; }
		IEnumerable<IPublishedContent> ContentAtRoot();
		IEnumerable<IPublishedContent> ChildrenOfType(IPublishedContent content, string contentTypeAlias, string culture = null);
		IPublishedContent GlobalSettingsPage { get; }
		IPublishedContent StartPage { get; }
		IPublishedContent SearchPage { get; }
		IPublishedContent Content(string id);
	}
}
