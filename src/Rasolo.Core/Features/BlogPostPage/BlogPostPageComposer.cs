using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPageComposer : IUserComposer
	{
			public void Compose(Composition composition)
			{
				composition.Register<IBlogPostPageViewModelFactory, BlogPostPageViewModelFactory>();
			}
		}
	}
