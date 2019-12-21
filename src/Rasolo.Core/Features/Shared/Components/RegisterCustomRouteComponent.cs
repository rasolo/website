using System.Web.Mvc;
using System.Web.Routing;
using Rasolo.Core.Features.BlogPage;
using Rasolo.Core.Features.Shared.Handlers;
using Umbraco.Core.Composing;
using Umbraco.Web;

namespace Rasolo.Core.Features.Shared.Components
{
	public class RegisterCustomRouteComponent : IComponent
	{
		#warning using hardcoded page ids for blogs. Fix
		public void Initialize()
		{
			 var blogPageControllerName = nameof(BlogPageController).Replace("Controller", string.Empty);

			RouteTable.Routes.MapUmbracoRoute("BlogPageRouteUmbraco", $"blogs/umbraco/{nameof(BlogPageController.Feed)}/{{id}}", new
			{
				controller = blogPageControllerName,
				action = nameof(BlogPageController.Feed),
				id = UrlParameter.Optional
			}, new BlogPageRouteHandler(1101)); //2094

			RouteTable.Routes.MapUmbracoRoute("BlogPageRouteEpiserver", $"blogs/episerver/{nameof(BlogPageController.Feed)}/{{id}}", new
			{
				controller = blogPageControllerName,
				action = nameof(BlogPageController.Feed),
				id = UrlParameter.Optional
			}, new BlogPageRouteHandler(1099)); //2112
		}

		public void Terminate()
		{
			// Nothing to terminate
		}
	}
}