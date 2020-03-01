using Rasolo.Core.Features.ArticlePage;
using Rasolo.Core.Features.BlogPage;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Core.Features.StartPage;
using Rasolo.Infrastructure.Repositories;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rasolo.Core.Features.Shared.Composers
{
	public class GenericClassesComposer : IUserComposer
	{
		public void Compose(Composition composition)
		{
			composition.Register<IBlogPostService, BlogPostService>();
			composition.Register<IBaseContentPageViewModelFactory<SiteMapPage.SiteMapPage>, BaseContentPageViewModelFactory<SiteMapPage.SiteMapPage>>();
			composition.Register<IArticlePageViewModelFactory, ArticlePageViewModelFactory>();
			composition.Register<IBlogPageViewModelFactory, BlogPageViewModelFactory>();
			composition.Register<IBaseContentPageViewModelFactory<BlogPostPage.BlogPostPage>, BaseContentPageViewModelFactory<BlogPostPage.BlogPostPage>>();
			composition.Register<IGlobalSettingsPageViewModelFactory, GlobalSettingsPagePageViewModelFactory>();
			composition.Register<IStartPageViewModelFactory, StartPageViewModelFactory>();
			composition.Register<IUmbracoService, UmbracoService>();
			composition.Register<ICommentsRepository, CommentsRepository>();
		}
	}
}