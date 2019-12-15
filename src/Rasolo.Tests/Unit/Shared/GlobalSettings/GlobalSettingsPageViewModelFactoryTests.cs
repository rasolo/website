using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
using Shouldly;
using System.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Shared.GlobalSettings
{
	internal class GlobalSettingsPageViewModelFactoryTests : UmbracoBaseTests
	{
		private GlobalSettingsPagePageViewModelFactory _sut;
		private const string CookiesNoticeText = "The cookies notice text";

		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var umbracoServiceMock = new Mock<IUmbracoService>();

			this._sut = new GlobalSettingsPagePageViewModelFactory(umbracoMapper, umbracoServiceMock.Object);
		}


		private GlobalSettingsPageViewModel SetUp(Mock<IPublishedProperty> property)
		{
			SetUp(DocumentTypeAlias.GlobalSettingsPage, property);

			return this._sut.CreateModel(null);
		}

		private void SetUp(string propertyAlias, Mock<IPublishedProperty> property)
		{
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var contentModel = this.SetupContent(propertyAlias, property);
			umbracoServiceMock.Setup(x => x.GetFirstPageByDocumentTypeAtRootLevel(It.IsAny<string>())).Returns(contentModel.Content);

			this._sut = new GlobalSettingsPagePageViewModelFactory(umbracoMapper, umbracoServiceMock.Object);
		}

		private GlobalSettingsPageViewModel SetUp(HttpCookieCollection httpCookieCollection)
		{
			SetUp(DocumentTypeAlias.GlobalSettingsPage, this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookiesNoticeTextAlias, string.Empty));

			return this._sut.CreateModel(httpCookieCollection);
		}


		[Test]
		[TestCase("The home text", "The home text")]
		[TestCase("Another home text", "Another home text")]
		public void Given_CreateModel_When_HomeTextGiven_Then_ReturnViewModelWithHomeText(string homeText, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.HomeTextAlias, homeText));

			viewModel.HomeText.ShouldBe(expected);
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void Given_CreateModel_When_HomeTextNullOrEmpty_Then_ReturnViewModelWithHomeTextEmptyString(string homeText, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.HomeTextAlias, homeText));

			viewModel.HomeText.ShouldBe(expected);

		}


		private GlobalSettingsPageViewModel SetUpCookiesNoticeText(string cookiesNoticeText)
		{
			return SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookiesNoticeTextAlias, cookiesNoticeText));
		}

		[Test]
		public void Given_CreateModel_When_GlobalSettingsPageCookiesNoticeTextGiven_Then_ReturnViewmodelWithCookiesNoticeText()
		{
			var viewModel = SetUpCookiesNoticeText(CookiesNoticeText);

			viewModel.CookiesNoticeText.ShouldBe(CookiesNoticeText);
		}


		//If the cookie is null, it means that the user has not accepted it, therefore the cookie notice should be shown.
		[Test]
		public void Given_CreateModel_When_CookiesNoticeCookieNull_Then_ReturnViewModelWithShowCookieNoticeTrue()
		{
			var viewModel = SetUp(httpCookieCollection: null);

			viewModel.ShowCookiesNotice.ShouldBe(true);
		}

		//If the cookie is already set/not null, it means that the user has accepted it, therefore the cookie notice should not be shown.
		[Test]
		public void Given_CreateModel_When_CookiesNoticeCookieSet_Then_ReturnViewModelWithShowCookieNoticeFalse()
		{
			var httpCookieCollectiWhen_ = new HttpCookieCollection
			{
				new HttpCookie(Core.Features.Shared.Constants.CookiesNotice.CookiesNoticeCookieName, "false")
			};

			var viewModel = SetUp(httpCookieCollectiWhen_);

			viewModel.ShowCookiesNotice.ShouldBe(false);
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void Given_CreateModel_When_CookiesNoticeTextNullOrEmpty_Then_ReturnViewModelWithCookiesNoticeTextEmptyString(string cookiesNoticeText, string expected)
		{
			var viewModel = SetUpCookiesNoticeText(cookiesNoticeText);

			viewModel.CookiesNoticeText.ShouldBe(expected);
		}

		[Test]
		[TestCase("The cookies notice text", "The cookies notice text")]
		[TestCase("Another cookies notice text", "Another cookies notice text")]
		public void Given_CreateModel_When_CookiesNoticeTextGiven_Then_ReturnViewModelWithCookiesNoticeText(string cookiesNoticeText, string expected)
		{
			var viewModel = SetUpCookiesNoticeText(cookiesNoticeText);

			viewModel.CookiesNoticeText.ShouldBe(expected);
		}

		[Test]
		[TestCase("The cookies accept text", "The cookies accept text")]
		[TestCase("Another cookies accept text", "Another cookies accept text")]
		public void Given_CreateModel_When_CookiesAcceptTextGiven_Then_ReturnViewModelWithCookiesAcceptText(string cookiesAcceptText, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookiesAcceptTextAlias, cookiesAcceptText));

			viewModel.CookiesAcceptText.ShouldBe(expected);
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void Given_CreateModel_When_CookiesAcceptTextNullOrEmpty_Then_ReturnViewModelWithCookiesAcceptTextEmptyString(string cookiesAcceptText, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookiesAcceptTextAlias, cookiesAcceptText));

			viewModel.CookiesAcceptText.ShouldBe(expected);
		}

		[Test]
		[TestCase(null, "/")]
		public void Given_CreateModel_When_CookiesLinkNull_Then_ReturnViewModelWithCookiesLinkPointingToHome(Link cookiesLink, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookiesLinkAlias, cookiesLink));

			viewModel.CookiesLink.Url.ShouldBe(expected);
		}
	}
}
