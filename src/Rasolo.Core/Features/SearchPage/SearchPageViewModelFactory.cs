using Rasolo.Core.Features.Shared.Abstractions;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Services.Abstractions.HttpRequest;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchPageViewModelFactory : BaseContentPageViewModelFactory<SearchPage>, ISearchPageViewModelFactory
	{
		private readonly IHttpUtility _httpUtility;
		private readonly IHttpRequest _httpRequest;

		public SearchPageViewModelFactory(IUmbracoMapper umbracoMapper, IUmbracoHelper umbracoHelper, IHttpUtility httpUtility, IHttpRequest httpRequest)
		 : base(umbracoMapper, umbracoHelper)
		{
			_httpUtility = httpUtility;
			_httpRequest = httpRequest;
		}
		public override SearchPage CreateModel(SearchPage viewModel, ContentModel contentModel)
		{
			viewModel = base.CreateModel(viewModel, contentModel);

			return viewModel;
		}

		public override void SetViewModelProperties(SearchPage viewModel, ContentModel contentModel)
		{
			base.SetViewModelProperties(viewModel, contentModel);

			viewModel.Query = this._httpUtility.UrlDecode(this._httpRequest.QueryString[QueryStrings.SearchQuery]);
		}
	}
}