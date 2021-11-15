using System.Linq;
using Anaximapper;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPageViewModelFactory : BaseContentPageViewModelFactory<BlogPage>, IBlogPageViewModelFactory
	{
		private readonly IPublishedContentMapper _anaxiMapper;
		private readonly IBlogPostService _blogPostService;

		public BlogPageViewModelFactory(IPublishedContentMapper anaxiMapper, IBlogPostService blogPostService, IUmbracoHelper umbracoHelper) : base(anaxiMapper, umbracoHelper)
		{
			this._anaxiMapper = anaxiMapper;
			this._blogPostService = blogPostService;
		}

		public override void SetViewModelProperties(BlogPage viewModel, ContentModel contentModel)
		{
			viewModel.BlogPosts = this._blogPostService.GetMappedBlogPosts(contentModel.Content.Children).ToList();
			viewModel.ShowPosts = viewModel.BlogPosts != null && viewModel.BlogPosts.Any();
			viewModel.Posts = viewModel.BlogPosts;
			//viewModel.ParentUrl = viewModel.Parent.Url;
			viewModel.ParentName = viewModel.Parent?.Name;
			viewModel.PostsTitle = "Latest posts";
		}

	}
}