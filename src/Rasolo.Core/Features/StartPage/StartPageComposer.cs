using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Core.Features.Shared.UI;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPageComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IStartPageViewModelFactory, StartPageViewModelFactory>();
		}
	}
}