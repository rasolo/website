using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		private readonly IUmbracoHelper _umbracoHelper;
		private readonly IHttpUtility _httpUtility;

		public SearchSurfaceController(IHttpUtility httpUtility, IUmbracoHelper umbracoHelper, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
			ServiceContext services, AppCaches appCaches,
			IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
			_umbracoHelper = umbracoHelper;
			_httpUtility = httpUtility;
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public RedirectToUmbracoPageResult Post(SearchParameters model)
		{
			var searchPage = this._umbracoHelper?.SearchPage;
			var querystring = new QueryString();

			if (!string.IsNullOrEmpty(model?.Query))
			{
				querystring.Add(QueryStrings.SearchQuery, this._httpUtility.UrlEncode(model.Query));
			}
			
			return RedirectToUmbracoPage(searchPage, querystring);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public RedirectToUmbracoPageResult Post2(SearchParameters model)
		{
			var searchPage = this._umbracoHelper?.SearchPage;
			var querystring = new QueryString();

			if (!string.IsNullOrEmpty(model?.Query))
			{
				querystring.Add(QueryStrings.SearchQuery, this._httpUtility.UrlEncode(model.Query));
			}
			
			return RedirectToUmbracoPage(searchPage, querystring);
		}
	}
}