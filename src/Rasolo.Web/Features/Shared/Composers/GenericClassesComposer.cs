//using Rasolo.Web.Features.ArticlePage;
//using Rasolo.Web.Features.BlogPage;
//using Rasolo.Web.Features.Shared.Compositions;
//using Rasolo.Web.Features.Shared.GlobalSettings;
//using Rasolo.Web.Features.Shared.Services;
//using Rasolo.Web.Features.StartPage;
//using Rasolo.Infrastructure.Repositories;
//using Umbraco.Core;
//using Umbraco.Core.Composing;

//namespace Rasolo.Web.Features.Shared.Composers
//{
//	public class GenericClassesComposer : IUserComposer
//	{
//		public void Compose(Composition composition)
//		{
//			composition.Register<IBlogPostService, BlogPostService>();
//			composition.Register<IBaseContentPageViewModelFactory<SiteMapPage.SiteMapPage>, BaseContentPageViewModelFactory<SiteMapPage.SiteMapPage>>();
//			composition.Register<IArticlePageViewModelFactory, ArticlePageViewModelFactory>();
//			composition.Register<IBlogPageViewModelFactory, BlogPageViewModelFactory>();
//			composition.Register<IBaseContentPageViewModelFactory<BlogPostPage.BlogPostPage>, BaseContentPageViewModelFactory<BlogPostPage.BlogPostPage>>();
//			composition.Register<IGlobalSettingsPageViewModelFactory, GlobalSettingsPagePageViewModelFactory>();
//			composition.Register<IStartPageViewModelFactory, StartPageViewModelFactory>();
//			composition.Register<IUmbracoService, UmbracoService>();
//			composition.Register<ICommentsRepository, CommentsRepository>();
//		}
//	}
//}