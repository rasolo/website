using Umbraco.Core;
using Umbraco.Core.Composing;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.Composers
{
	public class UmbracoMapperComposer : IUserComposer
	{
		public UmbracoMapper SetupMapper()
		{
			var umbracoMapper = new UmbracoMapper();
			return umbracoMapper;
		}

		public void Compose(Composition composition)
		{
			var mapper = this.SetupMapper();
			composition.Register<IUmbracoMapper>(mapper);
		}
	}
}