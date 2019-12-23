using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogPage;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Rasolo.Tests.Unit.Features.BlogPage
{
	internal class BlogPageControllerTests : BaseContentPageControllerTests<Core.Features.BlogPage.BlogPage>
	{
		private Mock<IBlogPageViewModelFactory> _blogViewModelFactory;

		[Test]
		public void Given_PageHasBlogPosts_When_IndexAction_Then_ReturnViewModelWithBlogPosts()
		{
			this._blogViewModelFactory = new Mock<IBlogPageViewModelFactory>();
			this._sut = new BlogPageController(this._umbracoMapper, this._blogViewModelFactory.Object);

			var blogPosts = new List<Core.Features.BlogPostPage.BlogPostPage>()
			{
				new Core.Features.BlogPostPage.BlogPostPage { Title = "First"},
				new Core.Features.BlogPostPage.BlogPostPage { Title = "Second"},
				new Core.Features.BlogPostPage.BlogPostPage { Title = "Third"},
			};

			var property = SetupPropertyValue("blogPosts", blogPosts);
			var contentModel = SetupContent(typeof(Core.Features.BlogPage.BlogPage).Name, property);

			this._blogViewModelFactory.Setup(x => x.CreateModel(It.IsAny<Core.Features.BlogPage.BlogPage>(), contentModel)).Returns(this._sut.MapModel(contentModel.Content));

			var viewModel = (Core.Features.BlogPage.BlogPage)((ViewResult)this._sut.Index(contentModel)).Model;

			viewModel.BlogPosts.First().Title.ShouldBe(blogPosts.First().Title);
		}
	}
}