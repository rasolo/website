using System;
using System.Linq;
using Anaximapper;
using Examine;
using Microsoft.AspNetCore.Http;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Rasolo.Services.Constants;
using Rasolo.Web.Features.SearchPage.Examine;
using Rasolo.Web.Features.Shared.Abstractions;
using Rasolo.Web.Features.Shared.Compositions;
using Rasolo.Web.Features.Shared.Constants;
using Rasolo.Web.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Web.Features.Shared.GlobalSettings;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Infrastructure.Examine;

namespace Rasolo.Web.Features.SearchPage
{
	public class SearchPageViewModelFactory : BaseContentPageViewModelFactory<SearchPageViewModel>,
		ISearchPageViewModelFactory
	{
		private readonly IPublishedContentMapper _anaxiMapper;
		private readonly IExamineSearcher _examineSearcher;
		private readonly GlobalSettingsPageViewModel _globalSettingsPageViewModel;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IHttpUtility _httpUtility;
		private readonly IUmbracoHelper _umbracoHelper;


		public SearchPageViewModelFactory
		(
			IPublishedContentMapper anaxiMapper, IHttpContextAccessor httpContextAccessor, IUmbracoHelper umbracoHelper,
			IHttpUtility httpUtility,
			IGlobalSettingsPageViewModelFactory globalSettingsPageViewModelFactory, IExamineSearcher examineSearcher)
			: base(anaxiMapper, umbracoHelper)
		{
			_anaxiMapper = anaxiMapper;
			_httpContextAccessor = httpContextAccessor;
			_umbracoHelper = umbracoHelper;
			_httpUtility = httpUtility;
			_examineSearcher = examineSearcher;
			_globalSettingsPageViewModel = globalSettingsPageViewModelFactory.CreateModel(null);
		}

		public override SearchPageViewModel CreateModel(SearchPageViewModel viewModel, ContentModel contentModel)
		{
			viewModel = base.CreateModel(viewModel, contentModel);

			return viewModel;
		}

		public override void SetViewModelProperties(SearchPageViewModel viewModel, ContentModel contentModel)
		{
			base.SetViewModelProperties(viewModel, contentModel);

			viewModel.CurrentPaginationPageNumber =
				int.Parse(string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Query[QueryStrings.Pagination])
					? "1"
					: _httpContextAccessor.HttpContext.Request.Query[QueryStrings.Pagination]);

			if (!string.IsNullOrEmpty(viewModel.Query))
			{
				Search(viewModel);
			}


			viewModel.NumberOfPages =
				(int)Math.Round(
					Convert.ToDecimal((double)viewModel.TotalItems / _globalSettingsPageViewModel.SearchResultsPerPage),
					MidpointRounding.AwayFromZero);
			viewModel.ShowPagination = viewModel.NumberOfPages >= 2;
			viewModel.PaginationSearchQuery =
				$"{viewModel.Url}?{QueryStrings.SearchQuery}={viewModel.Query}&{QueryStrings.Pagination}=";
			viewModel.ShowNextPagePaginationSymbol = viewModel.CurrentPaginationPageNumber < viewModel.NumberOfPages;
			viewModel.ShowPreviousPagePaginationSymbol = viewModel.CurrentPaginationPageNumber > 1;
			viewModel.NextPaginationPageUrl =
				$"{viewModel.PaginationSearchQuery}{viewModel.CurrentPaginationPageNumber + 1}";
			viewModel.PreviousPaginationPageUrl =
				$"{viewModel.PaginationSearchQuery}{viewModel.CurrentPaginationPageNumber + -1}";
			viewModel.ShowSearchResults = !string.IsNullOrEmpty(viewModel.Query);
			viewModel.SearchResultWord = viewModel.TotalItems == 1 ? "result" : "results";
		}

		public void Search(SearchPageViewModel viewModel)
		{
			var nodeTypes = new[] { DocumentTypeAlias.BlogPostPage };
			var properties = new[] { PropertyTypeAlias.Title, PropertyTypeAlias.Preamble };

			var searchResults = _examineSearcher.Search(viewModel.Query, 300, 0.4f,
				IndexTypes.Content, nodeTypes,
				properties);
			var searchResultItems = searchResults.Select(MapViewModels).ToList();

			viewModel.Results = searchResultItems
				.Skip((viewModel.CurrentPaginationPageNumber - 1) * _globalSettingsPageViewModel.SearchResultsPerPage)
				.Take(_globalSettingsPageViewModel.SearchResultsPerPage).ToList();
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

			_anaxiMapper.Map(content, viewModel);

			viewModel.ShowTeaserMediaAltText = !string.IsNullOrEmpty(viewModel.TeaserMediaAltText);

			return viewModel;
		}
	}
}