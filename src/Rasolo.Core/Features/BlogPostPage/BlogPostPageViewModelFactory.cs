using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPageViewModelFactory : IBlogPostPageViewModelFactory
	{
		private readonly IUmbracoMapper _umbracoMapper;

		public BlogPostPageViewModelFactory(IUmbracoMapper umbracoMapper)
		{
			_umbracoMapper = umbracoMapper;
		}

		public BlogPostPage CreateModel(ContentModel viewModel)
		{
			var blogPostPage = new BlogPostPage();
			this._umbracoMapper.Map(viewModel.Content, blogPostPage);

			return blogPostPage;
		}
	}
}