using Umbraco.Cms.Core.Models.PublishedContent;

namespace Rasolo.Web.Features.Shared.Controllers
{
	public interface IBasePageController<TModel>
	{
		TModel MapModel(IPublishedContent content);
	}
}
