using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace Rasolo.Core.Features.Shared.Composers
{
	public class RegisterCustomRouteComposer : ComponentComposer<RegisterCustomRouteComponent>
	{ }

	public class RegisterCustomRouteComponent : IComponent
	{
#warning using hardcoded page ids for blogs. Fix
		public void Initialize()
		{
			// Custom route to MyProductController which will use a node with a specific ID as the
			// IPublishedContent for the current rendering page
			RouteTable.Routes.MapUmbracoRoute("BlogPageRouteUmbraco", "blogs/umbraco/{id}", new
			{
				controller = "BlogPage",
				action = "Feed",
				id = UrlParameter.Optional
			}, new BlogPageRouteHandler(1085));

			RouteTable.Routes.MapUmbracoRoute("BlogPageRouteEpiserver", "blogs/episerver/{id}", new
			{
				controller = "BlogPage",
				action = "Feed",
				id = UrlParameter.Optional
			}, new BlogPageRouteHandler(1097));
		}

		public void Terminate()
		{
			// Nothing to terminate
		}
	}
}