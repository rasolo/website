using Umbraco.Web.Models;

namespace Rasolo.Core.Features.Shared.Compositions
{
	public interface IBaseContentPageViewModelFactory<TModel>
	{ 
		 TModel CreateModel(TModel viewModel, ContentModel contentModel);
		 void SetViewModelProperties(TModel viewModel, ContentModel contentModel);
	}
}
