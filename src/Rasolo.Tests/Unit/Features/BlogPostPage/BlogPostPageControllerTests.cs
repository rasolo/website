using System;
using System.Web.Mvc;
using NUnit.Framework;
using Rasolo.Core.Features.BlogPostPage;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Tests.Unit.Shared.BaseContentPage;
using Shouldly;
using Moq;
using Umbraco.Web.Models;


namespace Rasolo.Tests.Unit.Features.BlogPostPage
{
	internal class BlogPostPageControllerTests : BaseContentPageControllerTests<Core.Features.BlogPostPage.BlogPostPage>
	{
		public override void SetUp()
		{
			base.SetUp();
			this._sut = new BlogPostPageController(this._umbracoMapper, this._viewModelFactory.Object);
		}

		[Test]
		public void Given_BlogPostHasCreatedDate_When_IndexAction_Then_ReturnViewModelWithCreatedDate()
		{
			var expectedCreatedDate = new DateTime(2017, 01, 01);
			var property = SetupPropertyValue(BlogPostPagePropertyAlias.DateCreated, expectedCreatedDate);
			var content = SetupContentMock(DocumentTypeAlias.BlogPostPage, property);
			content.Setup(x => x.CreateDate).Returns(expectedCreatedDate);

			var contentModel = new ContentModel(content.Object);

			var viewModel = (Core.Features.BlogPostPage.BlogPostPage)((ViewResult)_sut.Index(contentModel)).Model;

			viewModel.CreatedDate.ShouldBe(expectedCreatedDate);
		}

		[Test]
		[TestCase("The page url", "The page url")]
		[TestCase("Another the page url", "Another the page url")]
		public void Given_BlogPostHasPageUrl_When_IndexAction_Then_ReturnViewModelWithPageUrl(string pageUrl, string expected)
		{
			var property = SetupPropertyValue(BlogPostPagePropertyAlias.PageUrl, pageUrl);
			var content = SetupContentMock(DocumentTypeAlias.BlogPostPage, property);
			content.Setup(x => x.Url).Returns(pageUrl);

			var contentModel = new ContentModel(content.Object);

			var viewModel = (Core.Features.BlogPostPage.BlogPostPage)((ViewResult)_sut.Index(contentModel)).Model;

			viewModel.PageUrl.ShouldBe(expected);
		}
	}
}
