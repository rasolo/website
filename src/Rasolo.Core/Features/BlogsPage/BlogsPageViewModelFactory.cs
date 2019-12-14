using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogsPage
{
	public class BlogsPageViewModelFactory : BaseContentPageViewModelFactory<BlogsPage>, IBlogsPageViewModelFactory
	{
		private readonly IUmbracoMapper _umbracoMapper;
		private readonly IUmbracoHelper _umbracoHelper;

		public BlogsPageViewModelFactory(IUmbracoMapper umbracoMapper, IUmbracoHelper umbracoHelper)
		{
			this._umbracoMapper = umbracoMapper;
			_umbracoHelper = umbracoHelper;
		}

		public BlogsPage CreateModel(ContentModel viewModel)
		{
			var blogsPage = new BlogsPage();
			blogsPage = base.CreateModel(blogsPage);

			this._umbracoMapper.Map(viewModel.Content, blogsPage);

			if (blogsPage.Children != null && blogsPage.Children.Any())
			{
				var blogPages = new List<BlogPage.BlogPage>();
				var blogChildren = this._umbracoHelper.ChildrenOfType(viewModel.Content,DocumentTypeAlias.BlogPage);
				this._umbracoMapper.MapCollection(blogChildren, blogPages);
				blogsPage.BlogPages = blogPages;
			}

			if (blogsPage.BlogPages?.Count() >= 1)
			{
				blogsPage.ShowBlogPages = true;
			}

			//For unit test, TeaserUrl is null because it is set through specific attributes only used in real run
			if (string.IsNullOrEmpty(blogsPage.TeaserUrl))
			{
				blogsPage.TeaserUrl = viewModel.Content.GetProperty(BlogsPagePropertyAlias.TeaserUrl)?.GetValue() as string;
			}

			return blogsPage;
		}
	}
}