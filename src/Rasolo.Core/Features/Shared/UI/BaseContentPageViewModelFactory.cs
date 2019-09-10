using System.Web;

namespace Rasolo.Core.Features.Shared.UI
{
	public class BaseContentPageViewModelFactory<TModel> : IBaseContentPageViewModelFactory<TModel> where TModel: IContentPage ,new() 
	{
		public TModel CreateModel(TModel viewModel)
		{
			viewModel.Title = viewModel.Title ?? string.Empty;
			viewModel.MainBody = viewModel.MainBody ?? new HtmlString(string.Empty);
			viewModel.ShowHeroImage = viewModel.HeroImage == null ? false : true;

			return viewModel;
		}
	}
}