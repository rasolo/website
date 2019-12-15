using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Tests.Unit.Shared.Services
{
	internal class BlogPostServiceTests : UmbracoBaseTests
	{
		private BlogPostService _sut;

		public override void SetUp()
		{
			base.SetUp();
		}

		//TODO: Add method for testing that teaser is returned first, then title and last page name.

		[Test]
		public void Given_Service_When_GetMappedBlogPosts_Then_ReturnsMappedBlogPosts()
		{
#warning Refractor. Create a base method for mocking multiple pages. Similar code with many pages are used elsewhere too.
			var umbracoServiceMock = new Mock<IUmbracoService>();

			var blogPostPageProperty1 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title1");
			var blogPostPage1 = this.SetupContentMock(DocumentTypeAlias.BlogPostPage, blogPostPageProperty1);

			var blogPostPageProperty2 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title2");
			var blogPostPage2 = this.SetupContentMock(DocumentTypeAlias.BlogPostPage, blogPostPageProperty2);

			var mockedBlogPostPages = new List<IPublishedContent> { blogPostPage1.Object, blogPostPage2.Object };

			blogPostPage1.Setup(x => x.UpdateDate).Returns(DateTime.Now);
			blogPostPage2.Setup(x => x.UpdateDate).Returns(DateTime.Now);

			umbracoServiceMock.Setup(x => x.GetAllPagesByDocumentTypeAtRootLevel(It.IsAny<string>())).Returns(mockedBlogPostPages);
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();

			this._sut = new BlogPostService(umbracoMapper);
			var blogPostPages = this._sut.GetMappedBlogPosts(mockedBlogPostPages);

			blogPostPages.Count().ShouldBeGreaterThan(1);
		}

		[Test]
		public void Given_Service_When_GetMappedBlogPosts_Then_ReturnsMappedBlogPostsByCreateDateDescending()
		{
#warning Refractor. Create a base method for mocking multiple pages. Similar code with many pages are used elsewhere too.
			var umbracoServiceMock = new Mock<IUmbracoService>();

			var blogPostPageProperty1 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title1");
			var blogPostPage1 = this.SetupContentMock(DocumentTypeAlias.BlogPostPage, blogPostPageProperty1);

			var blogPostPageProperty2 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title2");
			var blogPostPage2 = this.SetupContentMock(DocumentTypeAlias.BlogPostPage, blogPostPageProperty2);

			var blogPostPageProperty3 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title3");
			var blogPostPage3 = this.SetupContentMock(DocumentTypeAlias.BlogPostPage, blogPostPageProperty3);

			var mockedBlogPostPages = new List<IPublishedContent> { blogPostPage1.Object, blogPostPage2.Object, blogPostPage3.Object };

			blogPostPage1.Setup(x => x.CreateDate).Returns(new DateTime(2018, 01, 01));
			blogPostPage2.Setup(x => x.CreateDate).Returns(new DateTime(2017, 02, 02));
			blogPostPage3.Setup(x => x.CreateDate).Returns(new DateTime(2019, 03, 03));

			umbracoServiceMock.Setup(x => x.GetAllPagesByDocumentTypeAtRootLevel(It.IsAny<string>())).Returns(mockedBlogPostPages);
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();

			this._sut = new BlogPostService(umbracoMapper);
			var blogPostPages = this._sut.GetMappedBlogPosts(mockedBlogPostPages);

			blogPostPages.Select(x => x.CreateDate).ShouldBeInOrder(SortDirection.Descending);
		}
	}
}
