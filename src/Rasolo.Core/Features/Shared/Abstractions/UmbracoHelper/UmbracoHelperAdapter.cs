using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper
{
	public class UmbracoHelperAdapter : IUmbracoHelper
	{
		private readonly Umbraco.Web.UmbracoHelper _umbracoHelper;

		public UmbracoHelperAdapter(Umbraco.Web.UmbracoHelper umbracoHelper)
		{
			this._umbracoHelper = umbracoHelper;
		}

		public IEnumerable<IPublishedContent> ContentAtRoot()
		{
			return _umbracoHelper.ContentAtRoot();
		}

		public IEnumerable<IPublishedContent> ChildrenOfType(IPublishedContent content, string contentTypeAlias, string culture = null)
		{
			return content.ChildrenOfType(contentTypeAlias, culture);
		}
	}
}