using System.Web.Routing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Rasolo.Core.Features.Shared.Handlers
{
	internal class BlogPageRouteHandler : UmbracoVirtualNodeRouteHandler
	{
		private readonly int _blogPageId;

		public BlogPageRouteHandler(int blogPageId)
		{
			this._blogPageId = blogPageId;
		}

		protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
		{
			var content = umbracoContext.Content.GetById(_blogPageId);
			return content;
		}
	}
}