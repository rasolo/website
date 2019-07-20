using Rasolo.Core.Features.Shared.Services;
using Umbraco.Core.Composing;
using Umbraco.Core;

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