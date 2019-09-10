﻿using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.UI;
using Rasolo.Tests.Unit.Base;
using Shouldly;
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

			viewModel.Title.ShouldBe(expected);
		}

		[Test]
		[TestCase("The content page title", "The content page title")]
		[TestCase("Another content page title", "Another content page title")]
		public void Given_CreateModel_When_TitleGiven_ThenReturnViewModelWithTitle(string title, string expected)
		{
			var contentPage = new TModel() { Title = title };
			var viewModel = this._sut.CreateModel(contentPage);

			viewModel.Title.ShouldBe(expected);
		}

		[Test]
		[TestCase("The content page main body", "The content page main body")]
		[TestCase("Another content page main body", "Another content page main body")]
		public void Given_CreateModel_When_MainBodyGiven_ThenReturnViewModelWithMainBody(string mainBody, string expected)
		{
			var contentPage = new TModel() { MainBody = new HtmlString(mainBody) };
			var viewModel = this._sut.CreateModel(contentPage);

			viewModel.MainBody.ToString().ShouldBe(expected);
		}

		[Test]
		public void Given_CreateModel_When_HeroImageGiven_ThenReturnViewModelWithHeroImage()
		{
			var page = new Core.Features.Shared.UI.BaseContentPage();
			var viewModel = SetUpGetHeroImage(page);

			viewModel.HeroImage.ShouldBe(page.HeroImage);
		}

		Core.Features.Shared.UI.BaseContentPage SetUpGetHeroImage(Core.Features.Shared.UI.BaseContentPage page)
		{
			var imageMock = new Mock<IPublishedProperty>();
			imageMock.Setup(c => c.Alias).Returns(BaseContentPagePropertyAlias.HeroImage);
			imageMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			imageMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(SetupImage().Object);
			var contentModel = SetupContent(typeof(TModel).Name, imageMock);
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			umbracoMapper.Map(contentModel.Content, page);

			return this._sut.CreateModel(page);
		}

		[Test]
		public void Given_CreateModel_When_HeroImageGiven_ThenReturnViewModelWithShowHeroImageTrue()
		{
			var viewModel = SetUpGetHeroImage(new Core.Features.Shared.UI.BaseContentPage());

			viewModel.ShowHeroImage.ShouldBe(true);
		}

		[Test]
		public void Given_CreateModel_When_HeroImageNotGiven_ThenReturnViewModelWithShowHeroImageFalse()
		{
			var contentPage = new TModel() { HeroImage = null };
			var viewModel = this._sut.CreateModel(contentPage);

			viewModel.ShowHeroImage.ShouldBe(false);
		}

		[Test]
		public void Given_CreateModel_When_MainBodyNullOrEmpty_ThenReturnViewModelWithMainBodyEmptyString()
		{
			var contentPage = new TModel() { MainBody = null };
			var viewModel = this._sut.CreateModel(contentPage);

			viewModel.MainBody.ToString().ShouldBe(string.Empty);
		}
	}
}
