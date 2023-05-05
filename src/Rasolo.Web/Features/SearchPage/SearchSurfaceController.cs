using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rasolo.Services.Constants;
using Rasolo.Web.Features.SearchPage.Examine;
using Rasolo.Web.Features.Shared.Constants.PropertyTypeAlias;
using System.Linq;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.ActionResults;
using Umbraco.Cms.Web.Website.Controllers;

namespace Rasolo.Web.Features.SearchPage
{
	public class SearchSurfaceController : SurfaceController
	{
		private readonly IExamineSearcher _examineSearcher;

		public SearchSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
			ServiceContext services, AppCaches appCaches,
			IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, IExamineSearcher examineSearcher) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
			_examineSearcher = examineSearcher;
		}
		public RedirectToUmbracoPageResult Search(SearchPageViewModel model)
		{
			return RedirectToCurrentUmbracoPage(new QueryString("?q=" + model.Query));
		}

		public ActionResult GetSuggestions(string term)
		{
			var results = _examineSearcher.Search(term, 300, 0.8f, IndexTypes.Content, SearchableDocumentTypeAliases.Aliases, new string[] { PropertyTypeAlias.Title, PropertyTypeAlias.Preamble });

			var suggestions = results
				.SelectMany(x => x.Values)
				.Where(y => y.Key == "title")
				.Select(y => y.Value);
			return Json(suggestions);
		}
	}
}