using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchPageComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<ISearchPageViewModelFactory, SearchPageViewModelFactory>();
		}
	}
}