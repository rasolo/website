using Rasolo.Core.Features.Shared.CookiesNotice;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.Shared.Mappings
{
	public class CookiesNoticeMapperComposer : IUserComposer
	{
		//public CookiesNoticeViewModelFactory SetupCookiesNoticeViewModelFactory()
		//{
		//	var cookiesNoticeViewModelFactory = new CookiesNoticeViewModelFactory();
		//	return cookiesNoticeViewModelFactory;
		//}

		public void Compose(Composition composition)
		{
			composition.Register<ICookiesNoticeViewModelFactory, CookiesNoticeViewModelFactory>();
		}
	}
}