using Umbraco.Cms.Core.Models;

namespace Rasolo.Web.Features.Feed
{
	public interface IFeedPageViewModelFactory<TModel>
	{
		System.IO.MemoryStream  CreateModel(TModel viewModel, ContentModel contentModel);

	}
}