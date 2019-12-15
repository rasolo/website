using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
using Shouldly;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.Common.BaseDestinationTypes;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage
{
	internal class BaseContentPageControllerTests<TContentPage> : UmbracoBaseTests where TContentPage : Core.Features.Shared.Compositions.BaseContentPage, new()
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
			var content = SetupContentMock(typeof(TContentPage).Name, SetupPropertyValue("any", "any"), pageName: name);
			this._viewModelFactory.Setup(x => x.CreateModel(It.IsAny<TContentPage>()))
				.Returns(this._sut.MapModel(content.Object));
			var viewModel = (TContentPage)((ViewResult)_sut.Index(new ContentModel(content.Object))).Model;

			viewModel.Name.ShouldBe(expected);
		}

		[Test]
		[TestCase("Page title", "Page title")]
		[TestCase("Another Page title", "Another Page title")]
		public void Given_PageHasTitle_When_IndexAction_Then_ReturnViewModelWithPageTitle(string title, string expected)
		{
			var property = SetupPropertyValue(BaseContentPagePropertyAlias.Title, title);
			var contentModel = SetupContentMock(typeof(TContentPage).Name, property);

			this._mockedViewModel.Title = title;

			var viewModel = (TContentPage)((ViewResult)_sut.Index(new ContentModel(contentModel.Object))).Model;

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