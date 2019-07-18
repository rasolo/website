using Rasolo.Core.Features.StartPage;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.Mappings
{
	public class StartPageViewModelFactoryComposer : IUserComposer
	{
		public StartPageViewModelFactory SetupFactory()
		{
			var startPageViewModelFactory = new StartPageViewModelFactory();
			return startPageViewModelFactory;
		}

		public void Compose(Composition composition)
		{
			var mapper = this.SetupFactory();
			composition.Register<IStartPageViewModelFactory>(mapper);
		}
	}
}