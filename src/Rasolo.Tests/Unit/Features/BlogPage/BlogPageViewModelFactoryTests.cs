using Moq;
using NUnit.Framework;
using Rasolo.Tests.Unit.Shared.BaseContentPage;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Features.BlogPage
{
	internal class BlogPageViewModelFactoryTests : BaseContentPageViewModelFactoryTests<Core.Features.BlogPage.BlogPage>
	{
		private Core.Features.BlogPage.BlogPageViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();
			this._sut = new Core.Features.BlogPage.BlogPageViewModelFactory();
		}

		[Test]
		public void Given_CreateModelAndPageHasBlogPosts_When_ContentModelGiven_Then_ReturnViewModelWithBlogPosts()
		{
			var blogPosts = new List<Core.Features.BlogPostPage.BlogPostPage>()
			{
				new Core.Features.BlogPostPage.BlogPostPage { Title = "First"},
				new Core.Features.BlogPostPage.BlogPostPage { Title = "Second"},
				new Core.Features.BlogPostPage.BlogPostPage { Title = "Third"},
			};

			//var contentPage = new Core.Features.BlogPage.BlogPage { BlogPosts = blogPosts };
			var blogPostsMock = new Mock<IPublishedProperty>();
			blogPostsMock.Setup(c => c.Alias).Returns("whatever");
			blogPostsMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			blogPostsMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(blogPosts);

			var blogPostPageMock = this.SetupContentMock(typeof(Core.Features.BlogPostPage.BlogPostPage).Name, blogPostsMock);
			var blogPostPagesList = new List<IPublishedContent> { blogPostPageMock.Object };


			var blogPageMock = this.SetupContentMock(typeof(Core.Features.BlogPage.BlogPage).Name, blogPostsMock);
			blogPageMock.Setup(x => x.Children).Returns(blogPostPagesList);


			var viewModel = this._sut.CreateModel(new ContentModel(blogPageMock.Object));

			viewModel.BlogPosts.First().Title.ShouldBe(blogPosts.First().Title);
		}
	}
}
