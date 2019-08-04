namespace Rasolo.Core.Features.Shared.UI
{
	public interface IBaseContentPageViewModelFactory<TModel>
	{ 
		 TModel CreateModel(TModel viewModel);
	}
}
