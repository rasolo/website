using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogsPage;
using Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Features.BlogsPage
{
	internal class
		BlogsPageViewModelFactoryTests : BaseContentPageViewModelFactoryTests<Core.Features.BlogsPage.BlogsPage>
	{
		private BlogsPageViewModelFactory _sut;
		private Mock<IUmbracoHelper> _umbracoHelper;

		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			_umbracoHelper = new Mock<IUmbracoHelper>();


			_sut = new BlogsPageViewModelFactory(umbracoMapper, _umbracoHelper.Object);
		}

		[Test]
		public void Given_PageHasBlogPagesAsChildren_When_CreateModel_Then_ReturnsBlogPages()
		{
			var blogsPage = SetUpBlogPagesGetBlogPage();

			blogsPage.BlogPages.Count().ShouldBe(3);
		}

		private Core.Features.BlogsPage.BlogsPage SetUpBlogPagesGetBlogPage()
		{
			var blogPagesAsPublishedContent = new List<IPublishedContent>();
			for (var i = 0; i < 3; i++)
			{
				var blogPostsPropertyValue = SetupPropertyValue(BlogPagePropertyAlias.BlogPosts,
					Enumerable.Empty<Core.Features.BlogPostPage.BlogPostPage>());
				var content = SetupContent(DocumentTypeAlias.BlogPage, blogPostsPropertyValue);
				blogPagesAsPublishedContent.Add(content.Content);
			}

			var propertyValue = SetupPropertyValue("Name", "Blogs page");
			var contentMock = SetupContentMock(DocumentTypeAlias.BlogsPage, propertyValue);
			contentMock.Setup(x => x.Children).Returns(blogPagesAsPublishedContent);
			this._umbracoHelper
				.Setup(x => x.ChildrenOfType(It.IsAny<IPublishedContent>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(blogPagesAsPublishedContent);

			return _sut.CreateModel(null, new ContentModel(contentMock.Object));
		}

		[Test]
		public void Given_PageHasBlogPages_When_CreateModel_Then_ReturnsShowBlogPagesTrue()
		{
			var blogsPage = SetUpBlogPagesGetBlogPage();

			blogsPage.ShowPosts.ShouldBe(true);
		}

		[Test]
		public void Given_PageHasZeroBlogPages_When_CreateModel_Then_ReturnsShowBlogPagesFalse()
		{
			var propertyValue = SetupPropertyValue(BlogsPagePropertyAlias.BlogPages,
				Enumerable.Empty<Core.Features.BlogPage.BlogPage>());
			var content = SetupContent(DocumentTypeAlias.BlogsPage, propertyValue);
			var blogPage = _sut.CreateModel(null, content);

			blogPage.ShowPosts.ShouldBe(false);
		}
	}
}