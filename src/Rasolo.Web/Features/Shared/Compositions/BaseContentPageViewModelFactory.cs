using Anaximapper;
using Microsoft.AspNetCore.Html;
using Rasolo.Web.Features.Shared.Models;
using Rasolo.Services.Abstractions.UmbracoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;

namespace Rasolo.Web.Features.Shared.Compositions
{
	public class BaseContentPageViewModelFactory<TModel> : IBaseContentPageViewModelFactory<TModel> where TModel : BaseContentPage, new()
	{
		private readonly IPublishedContentMapper _anaxiMapper;
		private readonly IUmbracoHelper _umbracoHelper;

		public BaseContentPageViewModelFactory(IPublishedContentMapper anaxiMapper, IUmbracoHelper umbracoHelper)
		{
			_anaxiMapper = anaxiMapper;
			_umbracoHelper = umbracoHelper;
		}

		public virtual TModel CreateModel(TModel viewModel, ContentModel contentModel)
		{
			if (viewModel == null)
			{
				viewModel = new TModel();
			}

			SetViewModelProperties(viewModel, contentModel);
			return viewModel;
		}

		public virtual void SetViewModelProperties(TModel viewModel, ContentModel contentModel)
		{
			if (contentModel != null)
			{
				this._anaxiMapper.Map(contentModel.Content, viewModel);
				var breadCrumbs = new List<BreadCrumb>();
				this._anaxiMapper.MapCollection(this._umbracoHelper.AncestorsOrSelf(contentModel?.Content), breadCrumbs);
				breadCrumbs.Reverse();

				viewModel.BreadCrumbs = breadCrumbs;
			}

			viewModel.Title = viewModel.Title ?? string.Empty;
			viewModel.MainBody = viewModel.MainBody ?? new HtmlString(string.Empty);
			viewModel.ShowHeroImage = viewModel.HeroImage != null;

		}
	}
}
