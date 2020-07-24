using System;
using System.Linq;
using Examine;
using Rasolo.Core.Features.Shared.Abstractions;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Services.Abstractions.HttpRequest;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Umbraco.Core;
using Umbraco.Examine;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchPageViewModelFactory : BaseContentPageViewModelFactory<SearchPage>, ISearchPageViewModelFactory
	{
		private readonly IUmbracoMapper _umbracoMapper;
		private readonly IUmbracoHelper _umbracoHelper;
		private readonly IHttpUtility _httpUtility;
		private readonly IHttpRequest _httpRequest;
		private readonly IExamineManager _examineManager;

		public SearchPageViewModelFactory(IUmbracoMapper umbracoMapper, IUmbracoHelper umbracoHelper, IHttpUtility httpUtility, IHttpRequest httpRequest,
			IExamineManager examineManager)
		 : base(umbracoMapper, umbracoHelper)
		{
			_umbracoMapper = umbracoMapper;
			_umbracoHelper = umbracoHelper;
			_httpUtility = httpUtility;
			_httpRequest = httpRequest;
			_examineManager = examineManager;
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

			if (!string.IsNullOrEmpty(viewModel.Query))
			{
				Search(viewModel);
			}
		}

		public void Search(SearchPage viewModel)
		{
			if (!this._examineManager.TryGetIndex(Constants.UmbracoIndexes.ExternalIndexName, out IIndex index))
			{
				throw new InvalidOperationException($"No index found by name {Constants.UmbracoIndexes.ExternalIndexName}");
			}

			var searcher = index.GetSearcher();
			var query = searcher.CreateQuery(IndexTypes.Content);
			var operation = query.GroupedOr(new[] { "__NodeTypeAlias" }, new []{DocumentTypeAlias.BlogPostPage});
			var searchTerms = viewModel.Query.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
			foreach (string item in searchTerms)
			{
				operation.And().GroupedOr(new []{PropertyTypeAlias.Title, BlogPostPagePropertyAlias.TeaserUrl, PropertyTypeAlias.Preamble}, item.Fuzzy(0.6f));
			}

			var searchResults = operation.Execute(5);
			var searchResultItems = searchResults.Select(MapViewModels);
			viewModel.Results = searchResultItems.ToList();
			viewModel.TotalItems = viewModel.Results.Count;

		}

		private SearchResultItem MapViewModels(ISearchResult result)
		{
			if (result == null) return null;
			var content = this._umbracoHelper.Content(result.Id);

			if (content == null) return null;
			var viewModel = new SearchResultItem();

			this._umbracoMapper.Map(content, viewModel);

			viewModel.ShowTeaserMediaAltText = !string.IsNullOrEmpty(viewModel.TeaserMediaAltText);

			return viewModel;
		}
	}
}