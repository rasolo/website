﻿using Anaximapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rasolo.Web.Features.Shared.Abstractions;
using Rasolo.Web.Features.Shared.Compositions;
using Rasolo.Web.Features.Shared.Constants;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Rasolo.Web.Features.SearchPage
{
	public class SearchPageController : BaseContentPageController<Web.Features.SearchPage.SearchPageViewModel>
	{
		private readonly ISearchPageViewModelFactory _viewModelFactory;

		public SearchPageController(IPublishedContentMapper anaxiMapper, ISearchPageViewModelFactory viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, viewModelFactory, logger, compositeViewEngine, umbracoContextAccessor)

		{
			_viewModelFactory = viewModelFactory;
		}

		public IActionResult SearchPage(ContentModel contentModel, [FromQuery(Name = "q")] string query)
		{
			var mappedModel = this.MapModel(contentModel.Content);
			if (!string.IsNullOrEmpty(query))
			{
				mappedModel.Query = query;
			}
	
			var viewModel = this._viewModelFactory.CreateModel(mappedModel, contentModel);

			return View(viewModel);

		}
	}
}