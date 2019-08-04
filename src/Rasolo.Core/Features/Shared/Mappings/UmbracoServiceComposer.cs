using Rasolo.Core.Features.Shared.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.Shared.Mappings
{
	public class UmbracoServiceComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IUmbracoService, UmbracoService>();
		}
	}
}