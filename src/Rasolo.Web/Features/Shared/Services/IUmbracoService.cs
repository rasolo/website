using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Rasolo.Web.Features.Shared.Services
{
	public interface IUmbracoService
	{
		IPublishedContent GetFirstPageByDocumentTypeAtRootLevel(string documentTypeAlias);
		IEnumerable<IPublishedContent> GetFirstPageByDocumentTypeAtRootLevelAndDescendants(string documentTypeAlias);
		IEnumerable<IPublishedContent> GetAllPagesByDocumentTypeAtRootLevel(string documentTypeAlias);
		
	}
}
