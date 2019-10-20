using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Rasolo.Core.Features.Shared.Services
{
	public class UmbracoService : IUmbracoService
	{
		private readonly IUmbracoContextFactory _umbracoContextFactory;
		public UmbracoService(IUmbracoContextFactory context)
		{
			this._umbracoContextFactory = context;
		}

		/// <summary>
		/// Returns the first page that is found at the root level based on specified document type.</summary>
		/// <param name="documentTypeAlias"> The document type alias of the page to retrieve.</param>
		/// <returns>Returns the IPublishedContent.</returns>
		/// <seealso cref="string">
		/// You can use the cref attribute on any tag to reference a type or member 
		/// and the compiler will check that the reference exists. </seealso>
		public IPublishedContent GetFirstPageByDocumentTypeAtRootLevel(string documentTypeAlias)
		{
			return GetAllPagesByDocumentTypeAtRootLevel(documentTypeAlias).FirstOrDefault();
		}

		/// <summary>
		/// Returns the first page and its descendants that is found at the root level based on specified document type.</summary>
		/// <param name="documentTypeAlias"> The document type alias of the page to retrieve from.</param>
		/// <returns>Returns a collection of all the descendants that are found, including the found page at root level.</returns>
		/// <seealso cref="string">
		/// You can use the cref attribute on any tag to reference a type or member 
		/// and the compiler will check that the reference exists. </seealso>
		public IEnumerable<IPublishedContent> GetFirstPageByDocumentTypeAtRootLevelAndDescendants(string documentTypeAlias)
		{
			var firstPage = GetAllPagesByDocumentTypeAtRootLevel(documentTypeAlias).FirstOrDefault();

			if (firstPage == null)
			{
				return null;
			}

			var pages = firstPage.Descendants().ToList();
			pages.Insert(0, firstPage);

			return pages;
		}

		/// <summary>
		/// Returns all pages that is found at the root level and root level only based on specified document type.</summary>
		/// <param name="documentTypeAlias"> The document type alias of the pages to retrieve.</param>
		/// <returns>Returns the IPublishedContent.</returns>
		/// <seealso cref="string">
		/// You can use the cref attribute on any tag to reference a type or member 
		/// and the compiler will check that the reference exists. </seealso>
		public IEnumerable<IPublishedContent> GetAllPagesByDocumentTypeAtRootLevel(string documentTypeAlias)
		{
			using (var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
			{
				var cache = umbracoContextReference.UmbracoContext.Content;
				var contentType = cache.GetContentType(documentTypeAlias);
				return contentType == null ? null : cache.GetByContentType(contentType);
			}
		}
	}
}