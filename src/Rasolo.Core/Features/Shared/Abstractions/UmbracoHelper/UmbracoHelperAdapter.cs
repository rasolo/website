using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

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
	}
}