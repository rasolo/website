using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Core.Features.Shared.UI;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.Shared.UI
{
	public class BaseContentPageComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IBaseContentPageViewModelFactory<BaseContentPage>, BaseContentPageViewModelFactory<BaseContentPage>>();
		}
	}
}