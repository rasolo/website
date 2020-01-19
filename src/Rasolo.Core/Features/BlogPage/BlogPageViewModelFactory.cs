using System.Collections.Generic;
using System.Linq;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.DocumentTypeInterfaces;
using Rasolo.Core.Features.Shared.Services;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPageViewModelFactory : BaseContentPageViewModelFactory<BlogPage>, IBlogPageViewModelFactory
	{
		private readonly IUmbracoMapper _umbracoMapper;
		private readonly IBlogPostService _blogPostService;

		public BlogPageViewModelFactory(IUmbracoMapper umbracoMapper, IBlogPostService blogPostService)
		{
			this._umbracoMapper = umbracoMapper;
			this._blogPostService = blogPostService;
		}

		public override void SetViewModelProperties(BlogPage viewModel, ContentModel contentModel)
		{
			viewModel.BlogPosts = this._blogPostService.GetMappedBlogPosts(contentModel.Content.Children).ToList();
			viewModel.ShowPosts = viewModel.BlogPosts != null && viewModel.BlogPosts.Any();
			viewModel.Posts = viewModel.BlogPosts;
			viewModel.ParentUrl = viewModel.Parent?.Url;
			viewModel.ParentName = viewModel.Parent?.Name;
		}

	}
}