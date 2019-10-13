using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Core.Features.StartPage;
using Rasolo.Tests.Unit.Base;
using Shouldly;
using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Tests.Unit.Shared.StartPage
{
	internal class StartPageViewModelFactoryTests : UmbracoBaseTests
	{
		private StartPageViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();
			var umbracoServiceMock = new Mock<IUmbracoService>();

			var blogPageProperty1 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title1");
			var blogPage1 = this.SetupContentMock(DocumentTypeAlias.BlogPage, blogPageProperty1);

			var blogPageProperty2 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title2");
			var blogPage2 = this.SetupContentMock(DocumentTypeAlias.BlogPage, blogPageProperty2);

			blogPage1.Setup(x => x.UpdateDate).Returns(DateTime.Now);
			blogPage2.Setup(x => x.UpdateDate).Returns(DateTime.Now);
			var blogPages = new List<IPublishedContent> {blogPage1.Object, blogPage2.Object};

			umbracoServiceMock.Setup(x => x.GetAllPagesByDocumentTypeAtRootLevel(It.IsAny<string>())).Returns(blogPages);
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();

			this._sut = new StartPageViewModelFactory(umbracoMapper, umbracoServiceMock.Object);
		}

		[Test]
		public void Given_CreateModel_When_MethodCalled_Then_ReturnViewModelWithBlogPages()
		{
			var viewModel = this._sut.CreateModel(new Core.Features.StartPage.StartPage());
			viewModel.BlogPages.Count().ShouldBeGreaterThan(1);
		}

		[Test]
		public void Given_CreateModel_When_MethodCalled_Then_ReturnViewModelWithAllBlogPostPages()
		{
			var umbracoServiceMock = new Mock<IUmbracoService>();

			var blogPostPageProperty1 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title1");
			var blogPostPage1 = this.SetupContentMock(DocumentTypeAlias.BlogPostPage, blogPostPageProperty1);

			var blogPostPageProperty2 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title2");
			var blogPostPage2 = this.SetupContentMock(DocumentTypeAlias.BlogPostPage, blogPostPageProperty2);

			var blogPostPages = new List<IPublishedContent> { blogPostPage1.Object, blogPostPage2.Object };

			blogPostPage1.Setup(x => x.UpdateDate).Returns(DateTime.Now);
			blogPostPage2.Setup(x => x.UpdateDate).Returns(DateTime.Now);

			umbracoServiceMock.Setup(x => x.GetAllPagesByDocumentTypeAtRootLevel(It.IsAny<string>())).Returns(blogPostPages);
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();

			this._sut = new StartPageViewModelFactory(umbracoMapper, umbracoServiceMock.Object);
			var viewModel = this._sut.CreateModel(new Core.Features.StartPage.StartPage());

			viewModel.BlogPostPages.Count().ShouldBeGreaterThan(1);
		}
	}
}
