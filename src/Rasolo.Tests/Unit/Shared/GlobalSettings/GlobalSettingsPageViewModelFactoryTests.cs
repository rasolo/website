using System.Web;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
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
			umbracoServiceMock.Setup(x => x.GetFirstContentTypeAtRoot(It.IsAny<string>())).Returns(contentModel.Content);

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
		public void CreateModel_OnHomeTextGiven_ThenReturnViewModelWithHomeText(string homeText, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.HomeTextAlias, homeText));

			Assert.AreEqual(expected, viewModel.HomeText);
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void CreateModel_OnHomeTextNullOrEmpty_ThenReturnViewModelWithHomeTextEmptyString(string homeText, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.HomeTextAlias, homeText));

			Assert.AreEqual(expected, viewModel.HomeText);
		}


		private GlobalSettingsPageViewModel SetUpCookiesNoticeText(string cookiesNoticeText)
		{
			return SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookiesNoticeTextAlias, cookiesNoticeText));
		}

		[Test]
		public void CreateModel_OnGlobalSettingsPageCookiesNoticeTextGiven_ThenReturnViewmodelWithCookiesNoticeText()
		{
			var viewModel = SetUpCookiesNoticeText(CookiesNoticeText);
			Assert.AreEqual(CookiesNoticeText, viewModel.CookiesNoticeText);
		}


		//If the cookie is null, it means that the user has not accepted it, therefore the cookie notice should be shown.
		[Test]
		public void CreateModel_OnCookiesNoticeCookieNull_ThenReturnViewModelWithShowCookieNoticeTrue()
		{
			var viewModel = SetUp(httpCookieCollection: null);

			Assert.AreEqual(true, viewModel.ShowCookiesNotice);
		}

		//If the cookie is already set/not null, it means that the user has accepted it, therefore the cookie notice should not be shown.
		[Test]
		public void CreateModel_OnCookiesNoticeCookieSet_ThenReturnViewModelWithShowCookieNoticeFalse()
		{
			var httpCookieCollection = new HttpCookieCollection
			{
				new HttpCookie(Core.Features.Shared.Constants.CookiesNotice.CookiesNoticeCookieName, "false")
			};

			var viewModel = SetUp(httpCookieCollection);

			Assert.AreEqual(false, viewModel.ShowCookiesNotice);
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void CreateModel_OnCookiesNoticeTextNullOrEmpty_ThenReturnViewModelWithCookiesNoticeTextEmptyString(string cookiesNoticeText, string expected)
		{
			var viewModel = SetUpCookiesNoticeText(cookiesNoticeText);

			Assert.AreEqual(expected, viewModel.CookiesNoticeText);
		}

		[Test]
		[TestCase("The cookies notice text", "The cookies notice text")]
		[TestCase("Another cookies notice text", "Another cookies notice text")]
		public void CreateModel_OnCookiesNoticeTextGiven_ThenReturnViewModelWithCookiesNoticeText(string cookiesNoticeText, string expected)
		{
			var viewModel = SetUpCookiesNoticeText(cookiesNoticeText);

			Assert.AreEqual(expected, viewModel.CookiesNoticeText);
		}

		[Test]
		[TestCase("The cookies accept text", "The cookies accept text")]
		[TestCase("Another cookies accept text", "Another cookies accept text")]
		public void CreateModel_OnCookiesAcceptTextGiven_ThenReturnViewModelWithCookiesAcceptText(string cookiesAcceptText, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookiesAcceptTextAlias, cookiesAcceptText));

			Assert.AreEqual(expected, viewModel.CookiesAcceptText);
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void CreateModel_OnCookiesAcceptTextNullOrEmpty_ThenReturnViewModelWithCookiesAcceptTextEmptyString(string cookiesAcceptText, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookiesAcceptTextAlias, cookiesAcceptText));

			Assert.AreEqual(expected, viewModel.CookiesAcceptText);
		}

		[Test]
		[TestCase(null, "/")]
		public void CreateModel_OnCookiesLinkNull_ThenReturnViewModelWithCookiesLinkPointingToHome(Link cookiesLink, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookiesLinkAlias, cookiesLink));

			Assert.AreEqual(expected, viewModel.CookiesLink.Url);
		}
	}
}
