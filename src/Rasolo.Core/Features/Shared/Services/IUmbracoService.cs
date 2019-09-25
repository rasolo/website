using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Core.Features.Shared.Services
{
	public interface IUmbracoService
	{
		IPublishedContent GetFirstPageByDocumentTypeAtRootLevel(string alias);
		IEnumerable<IPublishedContent> GetAllPagesByDocumentTypeAtRootLevel(string alias);
		
	}
}
