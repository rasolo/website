using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogsPage;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;
using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Tests.Unit.Features.BlogsPage
{
	internal class BlogsPageControllerTests : BaseContentPageControllerTests<Core.Features.BlogsPage.BlogsPage>
	{
		public override void SetUp()
		{
			base.SetUp();
			this._sut = new BlogsPageController(this._umbracoMapper, this._viewModelFactory.Object);
		}

		[Test]
		public void Given_PageHasBlogPages_When_IndexAction_Then_ReturnViewModelWithBlogPages()
		{
			var blogPages = this.GetBlogPages();

			var property = SetupPropertyValue(BlogsPagePropertyAlias.BlogPages, blogPages);
			var content = SetupContent(DocumentTypeAlias.BlogPostPage, property);

			this._viewModelFactory.Setup(x => x.CreateModel(It.IsAny<Core.Features.BlogsPage.BlogsPage>())).Returns(_sut.MapModel(content.Content));
			var viewModel = (Core.Features.BlogsPage.BlogsPage)((ViewResult)_sut.Index(content)).Model;

			viewModel.BlogPages.ShouldBe(blogPages);
			blogPages.Remove(blogPages[0]);
			viewModel.BlogPages.Count().ShouldBe(blogPages.Count);
		}
	}
}
