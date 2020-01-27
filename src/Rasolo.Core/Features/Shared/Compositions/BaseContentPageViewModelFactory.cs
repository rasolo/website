using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper;
using Rasolo.Core.Features.Shared.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
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
			viewModel.BreadCrumbs = breadCrumbs;
		}
	}
}