using Rasolo.Services.Constants;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Rasolo.Services.Abstractions.UmbracoHelper

{
	public class UmbracoHelperAdapter : IUmbracoHelper
	{
		private readonly Umbraco.Web.UmbracoHelper _umbracoHelper;

		public UmbracoHelperAdapter(Umbraco.Web.UmbracoHelper umbracoHelper)
		{
			_umbracoHelper = umbracoHelper;
			umbracoHelper.Content(2).AncestorsOrSelf();
		}

		public IEnumerable<IPublishedContent> AncestorsOrSelf(IPublishedContent content)
		{
			return content.AncestorsOrSelf();
		}

		public IPublishedContent AssignedContentItem => _umbracoHelper.AssignedContentItem;

		public IEnumerable<IPublishedContent> ChildrenOfType(IPublishedContent content, string contentTypeAlias, string culture = null)
		{
			return content.ChildrenOfType(contentTypeAlias, culture);
		}

		public IEnumerable<IPublishedContent> ContentAtRoot()
		{
			return _umbracoHelper.ContentAtRoot();
		}

		public IPublishedContent GlobalSettingsPage => ContentAtRoot().OfTypes(DocumentTypeAlias.GlobalSettingsPage).FirstOrDefault();
		public IPublishedContent SearchPage => this.StartPage.ChildrenOfType(DocumentTypeAlias.SearchPage).FirstOrDefault();
		public IPublishedContent StartPage => this.ContentAtRoot().OfTypes(DocumentTypeAlias.StartPage).FirstOrDefault();

		public IPublishedContent Content(string id)
		{
			return _umbracoHelper.Content(id);
		}
	}
}