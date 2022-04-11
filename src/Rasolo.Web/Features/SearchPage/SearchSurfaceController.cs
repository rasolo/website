using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Rasolo.Web.Features.Shared.Abstractions;
using Rasolo.Web.Features.Shared.Constants;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.ActionResults;
using Umbraco.Cms.Web.Website.Controllers;

namespace Rasolo.Web.Features.SearchPage
{
	public class SearchSurfaceController : SurfaceController
	{
		public SearchSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
			ServiceContext services, AppCaches appCaches,
			IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider){}
		public RedirectToUmbracoPageResult Search(SearchPageViewModel model)
		{
			TempData.Add("SearchResults", JsonConvert.SerializeObject(model));

			return RedirectToCurrentUmbracoPage();
		}
	}
}