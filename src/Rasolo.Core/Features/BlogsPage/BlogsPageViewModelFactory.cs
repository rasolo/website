using System.Linq;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogsPage
{
	public class BlogsPageViewModelFactory : BaseContentPageViewModelFactory<BlogsPage>, IBlogsPageViewModelFactory
	{
		private readonly IUmbracoMapper _umbracoMapper;

		public BlogsPageViewModelFactory(IUmbracoMapper umbracoMapper)
		{
			this._umbracoMapper = umbracoMapper;
		}

		public BlogsPage CreateModel(ContentModel viewModel)
		{
			var blogsPage = new BlogsPage();
			this._umbracoMapper.Map(viewModel.Content, blogsPage);

			if (blogsPage.BlogPages?.Count() >= 1)
			{
				blogsPage.ShowBlogPages = true;
			}

			//For unit test, TeaserUrl is null because it is set through specific attributes only used in real run
			if (string.IsNullOrEmpty(blogsPage.TeaserUrl))
			{
				blogsPage.TeaserUrl = viewModel.Content.GetProperty(BlogsPagePropertyAlias.TeaserUrl).GetValue() as string;
			}

			return blogsPage;
		}
	}
}