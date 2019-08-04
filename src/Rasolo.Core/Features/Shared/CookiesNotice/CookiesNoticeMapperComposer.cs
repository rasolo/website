using Rasolo.Core.Features.Shared.CookiesNotice;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.Shared.Mappings
{
	public class CookiesNoticeMapperComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<ICookiesNoticeViewModelFactory, CookiesNoticeViewModelFactory>();
		}
	}
}