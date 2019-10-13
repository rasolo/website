using System.Web;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public class BaseContentPageViewModelFactory<TModel> : IBaseContentPageViewModelFactory<TModel> where TModel: IContentPage ,new() 
	{
		public virtual TModel CreateModel(TModel viewModel)
		{
			viewModel.Title = viewModel.Title ?? string.Empty;
			viewModel.MainBody = viewModel.MainBody ?? new HtmlString(string.Empty);
			viewModel.ShowHeroImage = viewModel.HeroImage != null;

			return viewModel;
		}
	}
}