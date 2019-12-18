using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper
{
	public class UmbracoHelperComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IUmbracoHelper>(component => new UmbracoHelperAdapter(component.GetInstance<Umbraco.Web.UmbracoHelper>()));
		}
	}
}