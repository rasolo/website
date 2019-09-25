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
		/// Returns the first document type that is found at the root level.</summary>
		/// <param name="contentTypeAlias"> The list on which to add to.</param>
		/// <returns>Returns the IPublishedContent.</returns>
		/// <seealso cref="string">
		/// You can use the cref attribute on any tag to reference a type or member 
		/// and the compiler will check that the reference exists. </seealso>
		public IPublishedContent GetFirstContentTypeAtRoot(string contentTypeAlias)
		{
			using (var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
			{
				var cache = umbracoContextReference.UmbracoContext.Content;
				var contentType = cache.GetContentType(contentTypeAlias);
				return contentType == null ? null : cache.GetByContentType(contentType).FirstOrDefault();
			}
		}
	}
}