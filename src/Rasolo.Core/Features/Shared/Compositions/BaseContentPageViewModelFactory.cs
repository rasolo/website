using System.Web;
using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public class BaseContentPageViewModelFactory<TModel> : IBaseContentPageViewModelFactory<TModel> where TModel: IContentPage ,new() 
	{
		public virtual TModel CreateModel(TModel viewModel, ContentModel contentModel)
		{
			if (viewModel == null)
			{
				viewModel = new TModel();
			}

			viewModel.Title = viewModel.Title ?? string.Empty;
			viewModel.MainBody = viewModel.MainBody ?? new HtmlString(string.Empty);
			viewModel.ShowHeroImage = viewModel.HeroImage != null;
			SetViewModelProperties(viewModel, contentModel);

			return viewModel;
		}

		public virtual void SetViewModelProperties(TModel viewModel, ContentModel contentModel)
		{
		}
	}
}