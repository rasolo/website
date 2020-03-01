using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Services.Abstractions.HttpRequest
{
	public class HttpRequestComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IHttpRequest, HttpRequestAdapter>();
		}
	}
}