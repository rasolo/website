using System;
using System.Web.Mvc;
using NUnit.Framework;
using Rasolo.Core.Features.BlogPostPage;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Shouldly;
using Moq;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
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
			this._viewModelFactory.Setup(x => x.CreateModel(It.IsAny<Core.Features.BlogPostPage.BlogPostPage>())).Returns(_sut.MapModel(contentModel.Content));
			var viewModel = (Core.Features.BlogPostPage.BlogPostPage)((ViewResult)_sut.Index(contentModel)).Model;

			viewModel.CreateDate.ShouldBe(expectedCreatedDate);
		}
	}
}
