using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Rasolo.Core.Features.Shared.Services
{
	public class UmbracoService : IUmbracoService
	{
		public readonly IUmbracoContextFactory _umbracoContextFactory;
		public UmbracoService(IUmbracoContextFactory context)
		{
			this._umbracoContextFactory = context;
		}
		public IPublishedContent GetFirstContentTypeAtRoot(string alias)
		{
			using (var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext())
			{
				var cache = umbracoContextReference.UmbracoContext.Content;
				var globalSettingsContentType = cache.GetContentType(alias);
				return cache.GetByContentType(globalSettingsContentType).FirstOrDefault();
			}
		}
	}
}