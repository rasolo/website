using System.Collections.Generic;
using System.Linq;
using Anaximapper;
using Rasolo.Web.Features.Shared.Compositions;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Rasolo.Services.Constants;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Rasolo.Web.Features.BlogsPage
{
	public class BlogsPageViewModelFactory : BaseContentPageViewModelFactory<BlogsPage>, IBlogsPageViewModelFactory
	{
		private readonly IPublishedContentMapper anaxiMapper;
		private readonly IUmbracoHelper _umbracoHelper;

		public BlogsPageViewModelFactory(IPublishedContentMapper anaxiMapper, IUmbracoHelper umbracoHelper) : base(anaxiMapper, umbracoHelper)
		{
			this.anaxiMapper = anaxiMapper;
			_umbracoHelper = umbracoHelper;
		}

		public override void SetViewModelProperties(BlogsPage viewModel, ContentModel contentModel)
		{

			this.anaxiMapper.Map(contentModel.Content, viewModel);

			if (viewModel.Children != null && viewModel.Children.Any())
			{
				var blogPages = new List<BlogPage.BlogPage>();
				var blogChildren = this._umbracoHelper.ChildrenOfType(contentModel.Content, DocumentTypeAlias.BlogPage);
				this.anaxiMapper.MapCollection(blogChildren, blogPages);
				viewModel.BlogPages = blogPages;
				
				foreach (var viewModelBlogPage in viewModel.BlogPages)
				{
					foreach (var blogChild in blogChildren)
					{
						if (blogChild.Id == viewModelBlogPage.Id)
						{
							viewModelBlogPage.Url = blogChild.Url();
						}
					}
				}
			}

			if (viewModel.BlogPages?.Count() >= 1)
			{
				viewModel.ShowPosts = true;
				viewModel.Posts = viewModel.BlogPages;
			}

			viewModel.PostsTitle = viewModel.Title;
		}
	}
}