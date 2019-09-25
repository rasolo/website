using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Core.Features.Shared.UI;
using Rasolo.Tests.Unit.Base;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Composers;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;
using Umbraco.Core.Models.PublishedContent;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Shouldly;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;

namespace Rasolo.Tests.Unit.Shared.BaseContentPage
{
	internal class BaseContentPageControllerTests<TContentPage> : UmbracoBaseTests where TContentPage : Core.Features.Shared.UI.BaseContentPage, new()
	{
		protected BaseContentPageController<TContentPage> _sut;
		protected Mock<IBaseContentPageViewModelFactory<TContentPage>> _viewModelFactory;
		protected UmbracoMapper _umbracoMapper;
		private readonly TContentPage _mockedViewModel = new TContentPage();

		public override void SetUp()
		{
			base.SetUp();
			this._viewModelFactory = new Mock<IBaseContentPageViewModelFactory<TContentPage>>();
			this._viewModelFactory.Setup(x => x.CreateModel(It.IsAny<TContentPage>())).Returns(this._mockedViewModel);
			this._umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._sut = new BaseContentPageController<TContentPage>(this._umbracoMapper, _viewModelFactory.Object);
		}

		[Test]
		public void Given_Controller_When_IndexAction_Then_ViewModelFactoryIsCalled()
		{
			var property = this.SetupPropertyValue("whatever", "whatever");
			var content = this.SetupContent(typeof(TContentPage).Name, property);

			this._sut.Index(content);

			this._viewModelFactory.Verify(x => x.CreateModel(It.IsAny<TContentPage>()), Times.Exactly(1));
		}

		[Test]
		public void Given_Controller_When_IndexAction_Then_ReturnsPageViewModel()
		{
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var property = this.SetupPropertyValue("whatever", "whatever");
			var content = this.SetupContent(typeof(TContentPage).Name, property);
			umbracoServiceMock.Setup(x => x.GetFirstPageByDocumentTypeAtRootLevel(It.IsAny<string>())).Returns(content.Content);
			this._sut = new BaseContentPageController<TContentPage>(this._umbracoMapper, this._viewModelFactory.Object);

			var returnedViewModel = (TContentPage)((ViewResult)_sut.Index(content)).Model;

			this._mockedViewModel.GetType().ShouldBe(returnedViewModel.GetType());
		}

		[Test]
		[TestCase("Page name", "Page name")]
		[TestCase("Another Page name", "Another Page name")]
		public void Given_PageHasName_When_IndexAction_Then_ReturnViewModelWithPageName(string name, string expected)
		{
			Content.SetupGet(x => x.Name).Returns(name);
			this._mockedViewModel.Name = name;

			var viewModel = (TContentPage)((ViewResult)_sut.Index(new ContentModel(Content.Object))).Model;

			viewModel.Name.ShouldBe(expected);
		}

		[Test]
		[TestCase("Page title", "Page title")]
		[TestCase("Another Page title", "Another Page title")]
		public void Given_PageHasTitle_When_IndexAction_Then_ReturnViewModelWithPageTitle(string title, string expected)
		{
			var property = SetupPropertyValue("title", title);
			var contentModel = SetupContent(typeof(TContentPage).Name, property);
			this._mockedViewModel.Title = title;

			var viewModel = (TContentPage)((ViewResult)_sut.Index(contentModel)).Model;

			viewModel.Title.ShouldBe(expected);
		}

		[Test]
		[TestCase("Main body", "Main body")]
		[TestCase("Another Main body", "Another Main body")]
		public void Given_PageHasMainBody_When_IndexAction_Then_ReturnViewModelWithMainBody(string mainBody, string expected)
		{
			var property = SetupPropertyValue("mainBody", mainBody);
			var contentModel = SetupContent(typeof(TContentPage).Name, property);
			this._mockedViewModel.MainBody = new MvcHtmlString(mainBody);

			var viewModel = (TContentPage)((ViewResult)_sut.Index(contentModel)).Model;

			viewModel.MainBody.ToString().ShouldBe(expected);
		}

		[Test]
		[TestCase("Teaser heading", "Teaser heading")]
		[TestCase("Another Teaser heading", "Another Teaser heading")]
		public void Given_PageHasTeaserHeading_When_IndexAction_Then_ReturnViewModelWithTeaserHeading(string teaserHeading, string expected)
		{
			var property = SetupPropertyValue("teaserHeading", teaserHeading);
			var contentModel = SetupContent(typeof(TContentPage).Name, property);
			this._mockedViewModel.TeaserHeading = teaserHeading;

			var viewModel = (TContentPage)((ViewResult)_sut.Index(contentModel)).Model;

			viewModel.TeaserHeading.ShouldBe(expected);
		}

		[Test]
		[TestCase("My preamble text", "My preamble text")]
		[TestCase("Another preamble text", "Another preamble text")]
		public void Given_PageHasTeaserPreamble_When_IndexAction_Then_ReturnViewModelWithTeaserPreamble(string teaserPreamble, string expected)
		{
			var property = SetupPropertyValue("teaserPreamble", teaserPreamble);
			var contentModel = SetupContent(typeof(TContentPage).Name, property);
			this._mockedViewModel.TeaserPreamble = new MvcHtmlString(teaserPreamble);

			var viewModel = (TContentPage)((ViewResult)this._sut.Index(contentModel)).Model;

			viewModel.TeaserPreamble.ToString().ShouldBe(expected);
		}

		TContentPage TestMediaReturnViewModel(string propertyAlias)
		{
			var mainImageMock = new Mock<IPublishedProperty>();
			mainImageMock.Setup(c => c.Alias).Returns(propertyAlias);
			mainImageMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			mainImageMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(SetupImage().Object);
			//This will automatically set the DomainWithUrl property for the media file. The url will be appended to this domain for the property.
			this._umbracoMapper.AssetsRootUrl = "http://www.mysite.com";
			var contentModel = SetupContent(typeof(TContentPage).Name, mainImageMock);

			this._viewModelFactory.Setup(x => x.CreateModel(It.IsAny<TContentPage>())).Returns(this._sut.MapModel(contentModel.Content));

			return (TContentPage)((ViewResult)this._sut.Index(contentModel)).Model;
		}

		[Test]
		public void Given_PageHasTeaserMedia_When_IndexAction_Then_ReturnViewModelWithTeaserMedia()
		{
			var viewModel = TestMediaReturnViewModel(BaseContentPagePropertyAlias.TeaserMedia);

			MediaFileShouldBe(viewModel.TeaserMedia);
		}

		void MediaFileShouldBe(MediaFile mediaFile)
		{
			mediaFile.ShouldNotBeNull();
			mediaFile.Id.ShouldBe(2000);
			mediaFile.Url.ShouldBe("/media/test.jpg");
			mediaFile.DomainWithUrl.ShouldBe("http://www.mysite.com/media/test.jpg");
			mediaFile.Name.ShouldBe("Test image");
			mediaFile.AltText.ShouldBe("Test image alt text");
			mediaFile.Width.ShouldBe(100);
			mediaFile.Height.ShouldBe(200);
			mediaFile.Size.ShouldBe(1000);
			mediaFile.FileExtension.ShouldBe(".jpg");
			mediaFile.DocumentTypeAlias.ShouldBe("Image");
		}

		[Test]
		public void Given_PageHasHeroImage_When_IndexAction_Then_ReturnViewModelWithHeroImage()
		{
			var viewModel = TestMediaReturnViewModel(BaseContentPagePropertyAlias.HeroImage);

			MediaFileShouldBe(viewModel.HeroImage);
		}
	}
}