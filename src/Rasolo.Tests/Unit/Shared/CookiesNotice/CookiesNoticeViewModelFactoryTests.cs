using System.Web;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Shared.CookiesNotice
{
	public class CookiesNoticeViewModelFactoryTests : UmbracoBaseTests
	{
		private CookiesNoticeViewModelFactory _sut;
		private readonly string _cookiesNoticeText = "The cookies notice text";

		public override void SetUp()
		{
			base.SetUp();

			SetUpCookiesNoticeText(this._cookiesNoticeText);
		}

		private CookiesNoticeViewModel SetUp(Mock<IPublishedProperty> property)
		{
			var httpCookieCollection = new HttpCookieCollection
			{
				new HttpCookie(Core.Features.Shared.Constants.CookiesNotice.CookiesNoticeCookieName, "false")
			};

			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var content = this.SetupContent(DocumentTypeAlias.GlobalSettingsPage, property);
			umbracoServiceMock.Setup(x => x.GetFirstContentTypeAtRoot(It.IsAny<string>())).Returns(content.Content);
			this._sut = new CookiesNoticeViewModelFactory(umbracoMapper, umbracoServiceMock.Object);
			return this._sut.CreateModel(httpCookieCollection);
		}

		private CookiesNoticeViewModel SetUpCookiesNoticeText(string cookiesNoticeText)
		{
			return SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookieNoticeText, cookiesNoticeText));
		}

		[Test]
		public void CreateModel_OnGlobalSettingsPageCookiesNoticeTextGiven_ThenReturnViewmodelWithCookiesNoticeText()
		{
			var viewModel = _sut.CreateModel(new HttpCookieCollection());
			Assert.AreEqual(this._cookiesNoticeText, viewModel.CookiesNoticeText);
		}


		//If the cookie is null, it means that the user has not accepted it, therefore the cookie notice should be shown.
		[Test]
		public void CreateModel_OnCookiesNoticeCookieNull_ThenReturnViewModelWithShowCookieNoticeTrue()
		{
			var httpCookieCollection = new HttpCookieCollection();
			var viewModel = _sut.CreateModel(httpCookieCollection);

			Assert.AreEqual(true, viewModel.ShowCookiesNotice);
		}

		//If the cookie is not null, it means that the user has accepted it, therefore the cookie notice should not be shown.
		[Test]
		public void CreateModel_OnCookiesNoticeCookieNotNull_ThenReturnViewModelWithShowCookieNoticeFalse()
		{
			var httpCookieCollection = new HttpCookieCollection
			{
				new HttpCookie(Core.Features.Shared.Constants.CookiesNotice.CookiesNoticeCookieName, "false")
			};

			var viewModel = _sut.CreateModel(httpCookieCollection);

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
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookieAcceptText, cookiesAcceptText));

			Assert.AreEqual(expected, viewModel.CookiesAcceptText);
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void CreateModel_OnCookiesAcceptTextNullOrEmpty_ThenReturnViewModelWithCookiesAcceptTextEmptyString(string cookiesAcceptText, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookieAcceptText, cookiesAcceptText));

			Assert.AreEqual(expected, viewModel.CookiesAcceptText);
		}

		[Test]
		[TestCase(null, "/")]
		public void CreateModel_OnCookiesLinkNull_ThenReturnViewModelWithCookiesLinkPointingToHome(Link cookiesLink, string expected)
		{
			var viewModel = SetUp(this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookieLink, cookiesLink));

			Assert.AreEqual(expected, viewModel.CookiesLink.Url);
		}
	}
}