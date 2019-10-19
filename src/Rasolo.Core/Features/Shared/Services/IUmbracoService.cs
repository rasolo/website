using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Core.Features.Shared.Services
{
	public interface IUmbracoService
	{
		IPublishedContent GetFirstPageByDocumentTypeAtRootLevel(string documentTypeAlias);
		IEnumerable<IPublishedContent> GetFirstPageByDocumentTypeAtRootLevelAndDescendants(string documentTypeAlias);
		IEnumerable<IPublishedContent> GetAllPagesByDocumentTypeAtRootLevel(string documentTypeAlias);
		
	}
}
