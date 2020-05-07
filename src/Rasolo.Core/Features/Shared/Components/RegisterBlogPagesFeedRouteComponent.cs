using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Rasolo.Core.Features.BlogPage;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Handlers;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Web;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.Components
{
	public class RegisterBlogPagesFeedRouteComponent : IComponent
	{
		private readonly IUmbracoContextFactory umbracoContextFactory;
		private readonly IUmbracoMapper umbracoMapper;

		public RegisterBlogPagesFeedRouteComponent(IUmbracoContextFactory umbracoContextFactory, IUmbracoMapper umbracoMapper)
		{
			this.umbracoContextFactory = umbracoContextFactory;
			this.umbracoMapper = umbracoMapper;
		}


		public void Initialize()
		{
			var blogPages = new List<BlogPage.BlogPage>();
			using (var umbracoContextReference = umbracoContextFactory.EnsureUmbracoContext())
			{
				var contentCache = umbracoContextReference.UmbracoContext.Content;

				var blogPagePublishedContentType = contentCache.GetContentType(DocumentTypeAlias.BlogPage);
				var blogPagesAsPublishedContent = contentCache.GetByContentType(blogPagePublishedContentType);

				if (blogPagesAsPublishedContent == null)
				{
					return;
				}

				umbracoMapper.MapCollection(blogPagesAsPublishedContent, blogPages);
			}

			var blogPageControllerName = nameof(BlogPageController).Replace("Controller", string.Empty);
			foreach (var blogPage in blogPages)
			{
				RouteTable.Routes.MapUmbracoRoute($"BlogPageRoute{blogPage.Name}",
					$"blogs/{blogPage.Name.ToFirstLower()}/{nameof(BlogPageController.Feed)}/{{id}}", new
					{
						controller = blogPageControllerName,
						action = nameof(BlogPageController.Feed),
						id = UrlParameter.Optional
					}, new BlogPageRouteHandler(blogPage.Id));
			}
		}

		public void Terminate()
		{
			// Nothing to terminate
		}
	}
}