using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.UI;
using Rasolo.Tests.Unit.Base;
using System.Web;
using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Tests.Unit.Shared.BaseContentPage
{
	internal class BaseContentPageViewModelFactoryTests <TModel> : UmbracoBaseTests where TModel : Core.Features.Shared.UI.BaseContentPage, new()
	{
		private BaseContentPageViewModelFactory<Core.Features.Shared.UI.BaseContentPage> _sut;

		public override void SetUp()
		{
			base.SetUp();
			this._sut = new BaseContentPageViewModelFactory<Core.Features.Shared.UI.BaseContentPage>();
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void Given_CreateModel_When_TitleNullOrEmpty_ThenReturnViewModelWithTitleEmptyString(string title, string expected)
		{
			var contentPage = new TModel() { Title = title };
			var viewModel = this._sut.CreateModel(contentPage);

			Assert.AreEqual(expected, viewModel.Title);
		}

		[Test]
		[TestCase("The content page title", "The content page title")]
		[TestCase("Another content page title", "Another content page title")]
		public void Given_CreateModel_When_TitleGiven_ThenReturnViewModelWithTitle(string title, string expected)
		{
			var contentPage = new TModel() { Title = title };
			var viewModel = this._sut.CreateModel(contentPage);

			Assert.AreEqual(viewModel.Title, expected);
		}

		[Test]
		[TestCase("The content page main body", "The content page main body")]
		[TestCase("Another content page main body", "Another content page main body")]
		public void Given_CreateModel_When_MainBodyGiven_ThenReturnViewModelWithMainBody(string mainBody, string expected)
		{
			var contentPage = new TModel() { MainBody = new HtmlString(mainBody) };
			var viewModel = this._sut.CreateModel(contentPage);

			Assert.AreEqual(viewModel.MainBody.ToString(), expected);
		}

		[Test]
		public void Given_CreateModel_When_HeroImageGiven_ThenReturnViewModelWithHeroImage()
		{
			var imageMock = new Mock<IPublishedProperty>();
			imageMock.Setup(c => c.Alias).Returns(BaseContentPagePropertyAlias.HeroImage);
			imageMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			imageMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(SetupImage().Object);
			var contentModel = SetupContent(typeof(TModel).Name, imageMock);
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var page = new Core.Features.Shared.UI.BaseContentPage();
			umbracoMapper.Map(contentModel.Content, page);

			var viewModel = this._sut.CreateModel(page);

			Assert.AreEqual(page.HeroImage, viewModel.HeroImage);
		}

		[Test]
		public void Given_CreateModel_When_MainBodyNullOrEmpty_ThenReturnViewModelWithMainBodyEmptyString()
		{
			var contentPage = new TModel() { MainBody = null };
			var viewModel = this._sut.CreateModel(contentPage);

			Assert.AreEqual(viewModel.MainBody.ToString(), string.Empty);
		}
	}
}
