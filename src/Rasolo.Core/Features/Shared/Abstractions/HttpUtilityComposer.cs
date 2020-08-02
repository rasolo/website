using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.Shared.Abstractions
{
	public class HttpUtilityComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IHttpUtility, HttpUtilityAdapter>();
		}
	}
}