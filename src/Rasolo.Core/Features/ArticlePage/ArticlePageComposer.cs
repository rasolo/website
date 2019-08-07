using Rasolo.Core.Features.ArticlePage;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.Shared.GlobalSettings
{
	public class ArticlePageComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IArticlePageViewModelFactory, ArticlePageViewModelFactory>();
		}
	}
}