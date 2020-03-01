using System.Collections.Generic;
using System.Linq;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Umbraco.Core;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPageViewModelFactory : BaseContentPageViewModelFactory<StartPage>, IStartPageViewModelFactory
	{
		private readonly IBlogPostService _blogPostService;
		private readonly IUmbracoMapper _umbracoMapper;
		private readonly IUmbracoService _umbracoService;


		public StartPageViewModelFactory(IUmbracoMapper umbracoMapper, IUmbracoService umbracoService,
			IBlogPostService blogPostService, IUmbracoHelper umbracoHelper) : base(umbracoMapper, umbracoHelper)
		{
			_umbracoMapper = umbracoMapper;
			_umbracoService = umbracoService;
			_blogPostService = blogPostService;
		}

		public override void SetViewModelProperties(StartPage viewModel, ContentModel contentModel)
		{
			base.SetViewModelProperties(viewModel, contentModel);
			SetBlogPagesOnViewModel(viewModel);
			viewModel.BlogPostPages = _blogPostService
				.GetMappedBlogPosts(_umbracoService
					.GetAllPagesByDocumentTypeAtRootLevel(DocumentTypeAlias.BlogPostPage)).ToList();

			viewModel.Title = viewModel.Title.StripHtml().Replace("\n", "<br />");

		}

		private void SetBlogPagesOnViewModel(StartPage viewModel)
		{
			var blogPagesAsPublishedContent =
				_umbracoService.GetAllPagesByDocumentTypeAtRootLevel(DocumentTypeAlias.BlogPage).ToList();

			viewModel.BlogPages = new List<BlogPage.BlogPage>();

			foreach (var blogPagePublishedContent in blogPagesAsPublishedContent)
			{
				var blogPage = new BlogPage.BlogPage();
				_umbracoMapper.Map(blogPagePublishedContent, blogPage);

				viewModel.BlogPages.Add(blogPage);
			}
		}
	}
}