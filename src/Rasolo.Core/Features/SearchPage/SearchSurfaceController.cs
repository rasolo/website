using System.Collections.Specialized;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Abstractions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Umbraco.Web.Mvc;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchSurfaceController : SurfaceController
	{
		private readonly IUmbracoHelper _umbracoHelper;
		private readonly IHttpUtility _httpUtility;

		public SearchSurfaceController(IUmbracoHelper umbracoHelper, IHttpUtility httpUtility)
		{
			_umbracoHelper = umbracoHelper;
			_httpUtility = httpUtility;
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Post(SearchParameters model)
		{
			var searchPageId = this._umbracoHelper?.SearchPage?.Id;
			if (!ModelState.IsValid || searchPageId == null)
			{
				return CurrentUmbracoPage();
			}

			var querystring = new NameValueCollection();

			if (!string.IsNullOrEmpty(model?.Query))
			{
				querystring.Add(QueryStrings.SearchQuery, this._httpUtility.UrlEncode(model.Query));
			}

			return RedirectToUmbracoPage(searchPageId.Value,querystring);
		}
	}
}