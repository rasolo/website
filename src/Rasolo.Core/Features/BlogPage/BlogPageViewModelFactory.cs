using System.Collections.Generic;
using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPageViewModelFactory : BaseContentPageViewModelFactory<BlogPage>, IBlogPageViewModelFactory
	{
		private readonly IUmbracoMapper _umbracoMapper;

		public BlogPageViewModelFactory(IUmbracoMapper umbracoMapper)
		{
			_umbracoMapper = umbracoMapper;
		}

		public BlogPage CreateModel(ContentModel viewModel)
		{
			var blogPage = new BlogPage();
			blogPage.BlogPosts = new List<BlogPostPage.BlogPostPage>();
			foreach (var child in viewModel.Content.Children)
			{
				var blogPost = new BlogPostPage.BlogPostPage();
				this._umbracoMapper.Map(child, blogPost);

				blogPage.BlogPosts.Add(blogPost);
			}

			return blogPage;
		}


	}
}