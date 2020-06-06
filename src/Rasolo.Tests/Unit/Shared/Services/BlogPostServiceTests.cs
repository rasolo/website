using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogPostPage;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
using Shouldly;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Shared.Services
{
	internal class BlogPostServiceTests : UmbracoBaseTests
	{
		private BlogPostService _sut;

		//TODO: Add method for testing that teaser is returned first, then title and last page name.

		[Test]
		public void Given_Service_When_GetMappedBlogPosts_Then_ReturnsMappedBlogPosts()
		{
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var mockedBlogPostPages = SetUpContentPages(2, DocumentTypeAlias.BlogPostPage).ToList();

			umbracoServiceMock.Setup(x => x.GetAllPagesByDocumentTypeAtRootLevel(It.IsAny<string>()))
				.Returns(mockedBlogPostPages);
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();

			var blogPostPageViewModelFactoryMock = new Mock<IBlogPostPageViewModelFactory>();
			var blogPostPage = new BlogPostPage();
			umbracoMapper.Map(mockedBlogPostPages.First(), blogPostPage);
			blogPostPageViewModelFactoryMock.Setup(x => x.CreateModel(It.IsAny<BlogPostPage>(), It.IsAny<ContentModel>())).Returns(blogPostPage);

			_sut = new BlogPostService(umbracoMapper, blogPostPageViewModelFactoryMock.Object);
			var blogPostPages = _sut.GetMappedBlogPosts(mockedBlogPostPages);

			blogPostPages.Count().ShouldBeGreaterThan(1);
		}

		[Test]
		public void Given_Service_When_GetMappedBlogPosts_Then_ReturnsMappedBlogPostsByCreateDateDescending()
		{
			var umbracoServiceMock = new Mock<IUmbracoService>();

			var mockedBlogPostPages = SetUpContentPagesMock(3, DocumentTypeAlias.BlogPostPage).ToList();
			for (var i = 0; i < mockedBlogPostPages.Count; i++)
			{
				mockedBlogPostPages[i].Setup(x => x.CreateDate).Returns(new DateTime(2018, 1 + i, 01));
			}

			var blogPostPagesPublishedContent = mockedBlogPostPages.Select(x => x.Object).ToList();

			umbracoServiceMock.Setup(x => x.GetAllPagesByDocumentTypeAtRootLevel(It.IsAny<string>()))
				.Returns(blogPostPagesPublishedContent);
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();

			var blogPostPageViewModelFactoryMock = new Mock<IBlogPostPageViewModelFactory>();
			var blogPostPage = new BlogPostPage();
			umbracoMapper.Map(mockedBlogPostPages.First().Object, blogPostPage);
			blogPostPageViewModelFactoryMock.Setup(x => x.CreateModel(It.IsAny<BlogPostPage>(), It.IsAny<ContentModel>())).Returns(blogPostPage);

			_sut = new BlogPostService(umbracoMapper, blogPostPageViewModelFactoryMock.Object);
			var blogPostPages = _sut.GetMappedBlogPosts(blogPostPagesPublishedContent);

			blogPostPages.Select(x => x.CreateDate).ShouldBeInOrder(SortDirection.Descending);
		}
	}
}