using System.Collections.Generic;
using System.Linq;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.MediaCropAliases.BlogPostPage;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.Services;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPageViewModelFactory : BaseContentPageViewModelFactory<StartPage>, IStartPageViewModelFactory
	{
		private readonly IUmbracoMapper _umbracoMapper;
		private readonly IUmbracoService _umbracoService;
		private readonly IBlogPostService _blogPostService;


		public StartPageViewModelFactory(IUmbracoMapper mapper, IUmbracoService umbracoService, IBlogPostService blogPostService)
		{
			this._umbracoMapper = mapper;
			this._umbracoService = umbracoService;
			this._blogPostService = blogPostService;
		}

		public override StartPage CreateModel(StartPage viewModel)
		{
			viewModel = base.CreateModel(viewModel);

			SetBlogPagesOnViewModel(viewModel);
			viewModel.BlogPostPages = this._blogPostService
                .GetMappedBlogPosts(_umbracoService
                .GetAllPagesByDocumentTypeAtRootLevel(DocumentTypeAlias.BlogPostPage)).ToList();

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
	}
}