using System.Collections.Generic;
using System.Linq;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.MediaCropAliases.BlogPostPage;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.Services;
using Umbraco.Web;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPageViewModelFactory : BaseContentPageViewModelFactory<StartPage>, IStartPageViewModelFactory
	{
		private readonly IUmbracoMapper _umbracoMapper;
		private readonly IUmbracoService _umbracoService;

		public StartPageViewModelFactory(IUmbracoMapper mapper, IUmbracoService umbracoService)
		{
			this._umbracoMapper = mapper;
			_umbracoService = umbracoService;
		}

		public override StartPage CreateModel(StartPage viewModel)
		{
			viewModel = base.CreateModel(viewModel);

			SetBlogPagesOnViewModel(viewModel);
			SetBlogPostPagesOnViewModel(viewModel);
			return viewModel;
		}

		//TODO: Refractor methods below, make dry
		private void SetBlogPagesOnViewModel(StartPage viewModel)
		{
			var blogPagesAsPublishedContent = _umbracoService.GetAllPagesByDocumentTypeAtRootLevel(DocumentTypeAlias.BlogPage).ToList();

			viewModel.BlogPages = new List<BlogPage.BlogPage>();

			foreach (var blogPagePublishedContent in blogPagesAsPublishedContent)
			{
				var blogPage = new BlogPage.BlogPage();
				this._umbracoMapper.Map(blogPagePublishedContent, blogPage);

				viewModel.BlogPages.Add(blogPage);
			}
		}

		private void SetBlogPostPagesOnViewModel(StartPage viewModel)
		{
			var blogPostPagesAsPublishedContent = _umbracoService.GetAllPagesByDocumentTypeAtRootLevel(DocumentTypeAlias.BlogPostPage).ToList();

			viewModel.BlogPostPages = new List<BlogPostPage.BlogPostPage>();

			foreach (var blogPostPagePublishedContent in blogPostPagesAsPublishedContent)
			{
				var blogPostPage = new BlogPostPage.BlogPostPage();
				this._umbracoMapper.Map(blogPostPagePublishedContent, blogPostPage);
				blogPostPage.CreatedDate = blogPostPagePublishedContent.CreateDate;
				blogPostPage.PageUrl = blogPostPagePublishedContent.Url;
				blogPostPage.TeaserHeading = !string.IsNullOrEmpty(blogPostPage.TeaserHeading)
					? blogPostPage.TeaserHeading
					: blogPostPage.Title;
				blogPostPage.TeaserMediaUrl =
					blogPostPagePublishedContent.GetCropUrl(BlogPostPagePropertyAlias.TeaserMedia,
						BlogPostPageMediaCropAliases.StartPage);
				viewModel.BlogPostPages.Add(blogPostPage);
			}

			viewModel.BlogPostPages = viewModel.BlogPostPages.OrderByDescending(x => x.CreatedDate).ToList();
		}
	}
}