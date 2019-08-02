using System.Web;

namespace Rasolo.Core.Features.Shared.UI
{
	public class BaseContentPageViewModelFactory<TModel> where TModel: BaseContentPage
	{
		public virtual TModel CreateModel(TModel viewModel)
		{
			viewModel.Title = viewModel.Title ?? string.Empty;
			viewModel.MainBody = viewModel.MainBody ?? new HtmlString(string.Empty);
			return viewModel;
		}
	}
}