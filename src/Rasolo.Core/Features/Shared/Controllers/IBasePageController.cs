using Umbraco.Cms.Core.Models.PublishedContent;

namespace Rasolo.Core.Features.Shared.Controllers
{
	public interface IBasePageController<TModel>
	{
		TModel MapModel(IPublishedContent content);
	}
}
