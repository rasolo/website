using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Compositions.TeaserPage;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Tests.Unit.Base;
using Shouldly;
using Umbraco.Core.Models.PublishedContent;
using UmbracoMapper = Zone.UmbracoMapper.V8.UmbracoMapper;

namespace Rasolo.Tests.Unit.Shared.Compositions.TeaserPage
{
	internal class TeaserPageControllerTests : UmbracoBaseTests
	{
		protected TeaserPageController Sut;
		protected UmbracoMapper UmbracoMapper;


		public override void SetUp()
		{
			base.SetUp();
			this.UmbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this.Sut = new TeaserPageController(this.UmbracoMapper);
		}

		[Test]
		[TestCase("Teaser heading", "Teaser heading")]
		[TestCase("Another Teaser heading", "Another Teaser heading")]
		public void Given_PageHasTeaserHeading_When_IndexAction_Then_ReturnViewModelWithTeaserHeading(string teaserHeading, string expected)
		{
			var property = SetupPropertyValue(TeaserPagePropertyAlias.TeaserHeading, teaserHeading);
			var contentModel = SetupContent(nameof(Core.Features.Shared.Compositions.TeaserPage.TeaserPage), property);

			var viewModel = (Core.Features.Shared.Compositions.TeaserPage.TeaserPage)((ViewResult)this.Sut.Index(contentModel)).Model;

			viewModel.TeaserHeading.ShouldBe(expected);
		}

		[Test]
		[TestCase("My preamble text", "My preamble text")]
		[TestCase("Another preamble text", "Another preamble text")]
		public void Given_PageHasTeaserPreamble_When_IndexAction_Then_ReturnViewModelWithTeaserPreamble(string teaserPreamble, string expected)
		{
			var property = SetupPropertyValue(TeaserPagePropertyAlias.TeaserPreamble, teaserPreamble);
			var contentModel = SetupContent(nameof(Core.Features.Shared.Compositions.TeaserPage.TeaserPage), property);

			var viewModel = (Core.Features.Shared.Compositions.TeaserPage.TeaserPage)((ViewResult)this.Sut.Index(contentModel)).Model;

			viewModel.TeaserPreamble.ShouldBe(expected);
		}

		[Test]
		public void Given_PageHasTeaserMedia_When_IndexAction_Then_ReturnViewModelWithTeaserMedia()
		{
			var teaserMediaMock = new Mock<IPublishedProperty>();
			teaserMediaMock.Setup(c => c.Alias).Returns(TeaserPagePropertyAlias.TeaserMedia);
			teaserMediaMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			teaserMediaMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(SetupImage().Object);
			//This will automatically set the DomainWithUrl property for the media file. The url will be appended to this domain for the property.
			this.UmbracoMapper.AssetsRootUrl = "http://www.mysite.com";

			var contentModel = SetupContent(nameof(Core.Features.Shared.Compositions.TeaserPage.TeaserPage), teaserMediaMock);

			var viewModel = (Core.Features.Shared.Compositions.TeaserPage.TeaserPage)((ViewResult)this.Sut.Index(contentModel)).Model;

			viewModel.TeaserMedia.ShouldNotBeNull();
			viewModel.TeaserMedia.Id.ShouldBe(2000);
			viewModel.TeaserMedia.Url.ShouldBe("/media/test.jpg");
			viewModel.TeaserMedia.DomainWithUrl.ShouldBe("http://www.mysite.com/media/test.jpg");
			viewModel.TeaserMedia.Name.ShouldBe("Test image");
			viewModel.TeaserMedia.AltText.ShouldBe("Test image alt text");
			viewModel.TeaserMedia.Width.ShouldBe(100);
			viewModel.TeaserMedia.Height.ShouldBe(200);
			viewModel.TeaserMedia.Size.ShouldBe(1000);
			viewModel.TeaserMedia.FileExtension.ShouldBe(".jpg");
			viewModel.TeaserMedia.DocumentTypeAlias.ShouldBe("Image");
		}
	}
}
