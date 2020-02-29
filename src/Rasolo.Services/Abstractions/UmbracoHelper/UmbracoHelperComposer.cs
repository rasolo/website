using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Services.Abstractions.UmbracoHelper

{
	public class UmbracoHelperComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IUmbracoHelper>(component => new UmbracoHelperAdapter(component.GetInstance<Umbraco.Web.UmbracoHelper>()));
		}
	}
}