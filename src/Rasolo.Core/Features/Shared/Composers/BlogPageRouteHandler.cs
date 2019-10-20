using System.Web.Routing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace Rasolo.Core.Features.Shared.Composers
{
	internal class BlogPageRouteHandler : UmbracoVirtualNodeRouteHandler
	{
		private int v;

		public BlogPageRouteHandler(int v)
		{
			this.v = v;
		}

		protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
		{
			var content = umbracoContext.Content.GetById(v);
			return content;
		}
	}
}