using Umbraco.Cms.Core.Models;

namespace Rasolo.Web.Features.Shared.Compositions
{
	public interface IBaseContentPageViewModelFactory<TModel>
	{
		TModel CreateModel(TModel viewModel, ContentModel contentModel);
	}
}
