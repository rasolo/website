using Rasolo.Core.Features.Shared.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.BlogsPage
{
	public class BlogsPageComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IBlogsPageViewModelFactory, BlogsPageViewModelFactory>();
		}
	}
}