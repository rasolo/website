using System;
using System.Linq;
using Examine;
using Rasolo.Core.Features.SearchPage.Examine;
using Rasolo.Core.Features.Shared.Abstractions;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Services.Abstractions.HttpRequest;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Umbraco.Examine;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchPageViewModelFactory : BaseContentPageViewModelFactory<SearchPage>, ISearchPageViewModelFactory
	{
		private readonly IExamineSearcher _examineSearcher;
		private readonly GlobalSettingsPageViewModel _globalSettingsPageViewModel;
		private readonly IHttpRequest _httpRequest;
		private readonly IHttpUtility _httpUtility;
		private readonly IUmbracoHelper _umbracoHelper;
		private readonly IUmbracoMapper _umbracoMapper;

		public SearchPageViewModelFactory
		(
			IUmbracoMapper umbracoMapper, IUmbracoHelper umbracoHelper, IHttpUtility httpUtility, IHttpRequest httpRequest, IGlobalSettingsPageViewModelFactory globalSettingsPageViewModelFactory, IExamineSearcher examineSearcher)
			: base(umbracoMapper, umbracoHelper)
		{
			_umbracoMapper = umbracoMapper;
			_umbracoHelper = umbracoHelper;
			_httpUtility = httpUtility;
			_httpRequest = httpRequest;
			_examineSearcher = examineSearcher;
			_globalSettingsPageViewModel = globalSettingsPageViewModelFactory.CreateModel(null);
		}

		public override SearchPage CreateModel(SearchPage viewModel, ContentModel contentModel)
		{
			viewModel = base.CreateModel(viewModel, contentModel);

			return viewModel;
		}

		public override void SetViewModelProperties(SearchPage viewModel, ContentModel contentModel)
		{
			base.SetViewModelProperties(viewModel, contentModel);

			viewModel.Query = _httpUtility.UrlDecode(_httpRequest.QueryString[QueryStrings.SearchQuery]);
			viewModel.CurrentPaginationPageNumber = int.Parse(string.IsNullOrEmpty(_httpRequest.QueryString[QueryStrings.Pagination]) ? "1" : _httpRequest.QueryString[QueryStrings.Pagination]);


			if (!string.IsNullOrEmpty(viewModel.Query))
			{
				Search(viewModel);
			}


			viewModel.NumberOfPages = (int) Math.Round(Convert.ToDecimal((double) viewModel.TotalItems / _globalSettingsPageViewModel.SearchResultsPerPage), MidpointRounding.AwayFromZero);
			viewModel.ShowPagination = viewModel.NumberOfPages >= 2;
			viewModel.PaginationSearchQuery = $"{viewModel.Url}?{QueryStrings.SearchQuery}={viewModel.Query}&{QueryStrings.Pagination}=";
			viewModel.ShowNextPagePaginationSymbol = viewModel.CurrentPaginationPageNumber < viewModel.NumberOfPages;
			viewModel.ShowPreviousPagePaginationSymbol = viewModel.CurrentPaginationPageNumber > 1;
			viewModel.NextPaginationPageUrl = $"{viewModel.PaginationSearchQuery}{viewModel.CurrentPaginationPageNumber + 1}";
			viewModel.PreviousPaginationPageUrl = $"{viewModel.PaginationSearchQuery}{viewModel.CurrentPaginationPageNumber + -1}";
			viewModel.ShowSearchResults = !string.IsNullOrEmpty(viewModel.Query);
		}

		public void Search(SearchPage viewModel)
		{
			var nodeTypes = new[] {DocumentTypeAlias.BlogPostPage};
			var properties = new[] {PropertyTypeAlias.Title, PropertyTypeAlias.Preamble};

			var searchResults = _examineSearcher.Search(viewModel.Query, 300, 0.4f,
				IndexTypes.Content, nodeTypes,
				properties);
			var searchResultItems = searchResults.Select(MapViewModels).ToList();

			viewModel.Results = searchResultItems.Skip((viewModel.CurrentPaginationPageNumber - 1) * _globalSettingsPageViewModel.SearchResultsPerPage).Take(_globalSettingsPageViewModel.SearchResultsPerPage).ToList();
			viewModel.TotalItems = searchResultItems.Count();
		}

		private SearchResultItem MapViewModels(ISearchResult result)
		{
			if (result == null)
			{
				return null;
			}

			var content = _umbracoHelper.Content(result.Id);

			if (content == null)
			{
				return null;
			}

			var viewModel = new SearchResultItem();

			_umbracoMapper.Map(content, viewModel);

			viewModel.ShowTeaserMediaAltText = !string.IsNullOrEmpty(viewModel.TeaserMediaAltText);

			return viewModel;
		}
	}
}