using System.Web;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Shared.CookiesNotice
{
	public class CookiesNoticeViewModelFactoryTests : UmbracoBaseTests
	{
		private readonly string _cookiesNoticeText = "The cookies notice text";
		private CookiesNoticeViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();

			SetUp(this._cookiesNoticeText);
		}
		
		private CookiesNoticeViewModel SetUp(string cookiesNoticeText = "", Link cookiesLink = default)
		{
			var httpCookieCollection = new HttpCookieCollection();
			httpCookieCollection.Add(
				new HttpCookie(Core.Features.Shared.Constants.CookiesNotice.CookiesNoticeCookieName, "false"));
			
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var property = this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookieNoticeText, cookiesNoticeText);
			var content = this.SetupContent(DocumentTypeAlias.GlobalSettingsPage, property);
			umbracoServiceMock.Setup(x => x.GetFirstContentTypeAtRoot(It.IsAny<string>())).Returns(content.Content);
			this._sut = new CookiesNoticeViewModelFactory(umbracoMapper, umbracoServiceMock.Object);
			return this._sut.CreateModel(httpCookieCollection);
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
			var httpCookieCollection = new HttpCookieCollection();
			httpCookieCollection.Add(
				new HttpCookie(Core.Features.Shared.Constants.CookiesNotice.CookiesNoticeCookieName, "false"));
			var viewModel = _sut.CreateModel(httpCookieCollection);

			Assert.AreEqual(false, viewModel.ShowCookiesNotice);
		}
		
		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void CreateModel_OnCookiesNoticeTextNullOrEmpty_ThenReturnViewModelWithCookiesNoticeTextEmptyString(string cookiesNoticeText, string expected)
		{
			var viewModel = SetUp(cookiesNoticeText);

			Assert.AreEqual(expected, viewModel.CookiesNoticeText);
		}
		
		[Test]
		[TestCase("The cookies notice text", "The cookies notice text")]
		[TestCase("Another the cookies notice text", "Another the cookies notice text")]
		public void CreateModel_OnCookiesNoticeTextSet_ThenReturnViewModelWithCookiesNoticeText(string cookiesNoticeText, string expected)
		{

			var viewModel = SetUp(cookiesNoticeText);

			Assert.AreEqual(expected, viewModel.CookiesNoticeText);
		}

		[Test]
		[TestCase(null, "/")]
		public void CreateModel_OnCookiesLinkNull_ThenReturnViewModelWithCookiesLinkPointingToHome(Link cookiesLink, string expected)
		{
			var viewModel = SetUp(cookiesLink: cookiesLink);

			Assert.AreEqual(expected, viewModel.CookiesLink.Url);
		}
	}
}