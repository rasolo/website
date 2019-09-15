using Rasolo.Core.Features.Shared.UI;
using System.Linq;
using Umbraco.Web.Models;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPageViewModelFactory : BaseContentPageViewModelFactory<BlogPage>, IBlogPageViewModelFactory
	{
		public override BlogPage CreateModel(BlogPage viewModel)
		{
			viewModel = base.CreateModel(viewModel);

			//viewModel.BlogPosts = viewModel.
			return viewModel;
		}

		public BlogPage CreateModel(ContentModel viewModel)
		{
		//	blogPage = base.CreateModel(blogPage);
			//blogPage.BlogPosts = viewModel.Content.Children
			//viewModel.Content.Children.wh

			return new BlogPage();

		}

	
	}
}