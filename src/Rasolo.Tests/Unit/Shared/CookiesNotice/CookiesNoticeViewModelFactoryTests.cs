using System.Web;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;

namespace Rasolo.Tests.Unit.Shared.CookiesNotice
{
	public class CookiesNoticeViewModelFactoryTests : UmbracoBaseTests
	{
		private readonly string _cookieText = "The cookie text";
		private CookiesNoticeViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();

			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var property = SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookieNoticeText, _cookieText);
			var content = SetupContent(DocumentTypeAlias.GlobalSettingsPage, property);
			umbracoServiceMock.Setup(x => x.GetFirstContentTypeAtRoot(It.IsAny<string>())).Returns(content.Content);
			this._sut = new CookiesNoticeViewModelFactory(umbracoMapper, umbracoServiceMock.Object);
		}

		[Test]
		public void CreateModel_OnGlobalSettingsPageCookieNoticeTextGiven_ThenReturnViewodelWithCookiesNoticeText()
		{
			var viewModel = _sut.CreateModel(new HttpCookieCollection());
			Assert.AreEqual(_cookieText, viewModel.CookieNoticeText);
		}


		//If the cookie is null, it means that the user has not accepted it, therefore the cookie notice should be shown.
		[Test]
		public void CreateModel_OnCookieNoticeCookieNull_ThenReturnViewModelWithShowCookieNoticeTrue()
		{
			var httpCookieCollection = new HttpCookieCollection();
			var viewModel = _sut.CreateModel(httpCookieCollection);

			Assert.AreEqual(true, viewModel.ShowCookiesNotice);
		}

		//If the cookie is not null, it means that the user has accepted it, therefore the cookie notice should not be shown.
		[Test]
		public void CreateModel_OnCookieNoticeCookieNotNull_ThenReturnViewModelWithShowCookieNoticeFalse()
		{
			var httpCookieCollection = new HttpCookieCollection();
			httpCookieCollection.Add(
				new HttpCookie(Core.Features.Shared.Constants.CookiesNotice.CookiesNoticeCookieName, "false"));
			var viewModel = _sut.CreateModel(httpCookieCollection);

			Assert.AreEqual(false, viewModel.ShowCookiesNotice);
		}
	}
}