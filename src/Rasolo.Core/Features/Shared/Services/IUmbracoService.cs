using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Core.Features.Shared.Services
{
	public interface IUmbracoService
	{
		IPublishedContent GetFirstContentTypeAtRoot(string alias);
	}
}
