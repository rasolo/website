using System.Collections.Generic;
using System.Linq;
using Anaximapper;
using Rasolo.Web.Features.Shared.Compositions;
using Rasolo.Web.Features.Shared.Services;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Rasolo.Services.Constants;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;
using Rasolo.Web.Features.Shared.Models;

namespace Rasolo.Web.Features.StartPage
{
	public class StartPageViewModelFactory : BaseContentPageViewModelFactory<StartPage>, IStartPageViewModelFactory
	{
		private readonly IBlogPostService _blogPostService;
		private readonly IPublishedContentMapper anaxiMapper;
		private readonly IUmbracoService _umbracoService;


		public StartPageViewModelFactory(IPublishedContentMapper anaxiMapper, IUmbracoService umbracoService,
			IBlogPostService blogPostService, IUmbracoHelper umbracoHelper) : base(anaxiMapper, umbracoHelper)
		{
			this.anaxiMapper = anaxiMapper;
			_umbracoService = umbracoService;
			_blogPostService = blogPostService;
		}

		public override void SetViewModelProperties(StartPage viewModel, ContentModel contentModel)
		{
			base.SetViewModelProperties(viewModel, contentModel);
			SetBlogPagesOnViewModel(viewModel);
			viewModel.BlogPostPages = _blogPostService
				.GetMappedBlogPosts(_umbracoService
					.GetAllPagesByDocumentTypeAtRootLevel(DocumentTypeAlias.BlogPostPage)).Take(4).ToList();

			SetButtons(viewModel);

			viewModel.Title = viewModel.Title.StripHtml().Replace("\n", "<br />");

		}

		private static void SetButtons(StartPage viewModel)
		{
			viewModel.TopButton = new ButtonViewModel
			{
				ContainerModifierClass = null,
				ButtonIdOne = "projects-teaser",
				ButtonTextOne = "Projects",
				ButtonIdTwo = "posts-teaser",
				ButtonTextTwo = "Posts"
			};

			viewModel.BottomButton = new ButtonViewModel
			{
				ContainerModifierClass = "button-container--mt",
				ButtonIdOne = "js-load-more-posts",
				ButtonTextOne = "Load more",
				ButtonIdTwo = "js-load-all-posts",
				ButtonTextTwo = "Load all"
			};
		}

		private void SetBlogPagesOnViewModel(StartPage viewModel)
		{
			var blogPagesAsPublishedContent =
				_umbracoService.GetAllPagesByDocumentTypeAtRootLevel(DocumentTypeAlias.BlogPage).ToList();

			viewModel.BlogPages = new List<BlogPage.BlogPage>();

			foreach (var blogPagePublishedContent in blogPagesAsPublishedContent)
			{
				var blogPage = new BlogPage.BlogPage();
				this.anaxiMapper.Map(blogPagePublishedContent, blogPage);

				viewModel.BlogPages.Add(blogPage);
			}
		}
	}
}