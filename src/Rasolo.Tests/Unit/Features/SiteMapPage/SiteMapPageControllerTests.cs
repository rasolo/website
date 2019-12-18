using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Core.Features.SiteMapPage;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Tests.Unit.Features.SiteMapPage
{
	internal class SiteMapPageControllerTests : BaseContentPageControllerTests<Core.Features.SiteMapPage.SiteMapPage>
	{
		[Test]
		public void Given_Controller_When_IndexAction_Then_ReturnsViewModelWithAllPages()
		{
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var blogPageProperty1 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title1");
			var blogPage1 = this.SetupContentMock(DocumentTypeAlias.BlogPage, blogPageProperty1);

			var blogPageProperty2 = this.SetupPropertyValue(BaseContentPagePropertyAlias.Title, "title2");
			var blogPage2 = this.SetupContentMock(DocumentTypeAlias.BlogPage, blogPageProperty2);

			blogPage1.Setup(x => x.UpdateDate).Returns(DateTime.Now);
			blogPage2.Setup(x => x.UpdateDate).Returns(DateTime.Now);
			var blogPages = new List<IPublishedContent> { blogPage1.Object, blogPage2.Object };
			umbracoServiceMock.Setup(x => x.GetFirstPageByDocumentTypeAtRootLevelAndDescendants(It.IsAny<string>()))
				.Returns(blogPages);
			var property = this.SetupPropertyValue("any", "any");
			var content = this.SetupContent(nameof(Core.Features.SiteMapPage.SiteMapPage), property);

			var controller = new SiteMapPageController(this._umbracoMapper, this._viewModelFactory.Object, umbracoServiceMock.Object);
			var viewModel = (Core.Features.SiteMapPage.SiteMapPage)((ViewResult)controller.Index(content)).Model;

			var blogPagesContentPage = new List<BaseContentPage>();
			this._umbracoMapper.MapCollection(blogPages, blogPagesContentPage);

			viewModel.AllPages.ToList().First().Title.ShouldBe(blogPagesContentPage.First().Title);
			viewModel.AllPages.ToList()[1].Title.ShouldBe(blogPagesContentPage[1].Title);
		}
	}
}
