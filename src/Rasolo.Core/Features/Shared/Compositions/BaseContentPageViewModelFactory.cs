using System.Collections.Generic;
using System.Web;
using Rasolo.Core.Features.Shared.Models;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public class BaseContentPageViewModelFactory<TModel> : IBaseContentPageViewModelFactory<TModel> where TModel: BaseContentPage ,new() 
	{
		private readonly IUmbracoMapper _umbracoMapper;
		private readonly IUmbracoHelper _umbracoHelper;

		public BaseContentPageViewModelFactory(IUmbracoMapper umbracoMapper, IUmbracoHelper umbracoHelper)
		{
			_umbracoMapper = umbracoMapper;
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
			viewModel.Title = viewModel.Title ?? string.Empty;
			viewModel.MainBody = viewModel.MainBody ?? new HtmlString(string.Empty);
			viewModel.ShowHeroImage = viewModel.HeroImage != null;
			var breadCrumbs = new List<BreadCrumb>();
			this._umbracoMapper.MapCollection(this._umbracoHelper.AncestorsOrSelf(contentModel?.Content), breadCrumbs);
			breadCrumbs.Reverse();

			viewModel.BreadCrumbs = breadCrumbs;
		}
	}
}