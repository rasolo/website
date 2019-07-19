using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Tests.Unit.Base;
using Rasolo.Core.Features.Shared.Settings;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Mappings;

namespace Rasolo.Tests.Unit.Shared.CookiesNotice
{
	public class CookiesNoticeControllerTests : UmbracoBaseTests
	{
		private CookiesNoticeController _sut;
		//private Mock<Rasolo.Core.Features.Shared.Settings.IGlobalSettings> _globalSettings;

		public override void SetUp()
		{
			//TODO: Create sitesetting in admin with cookie in it and render cookiebar
			base.SetUp();
			//this._globalSettings = new Mock<IGlobalSettings>();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			//var cookiesNoticeViewModelFactory = new CookiesNoticeMapperComposer().Compose();
			var cookiesNoticeViewModelFactory = new Mock<CookiesNoticeViewModelFactory>();

			this._sut = new CookiesNoticeController(umbracoMapper, cookiesNoticeViewModelFactory.Object);
		}

		[Test]
		[TestCase("My cookies notice text", "My cookies notice text")]
		[TestCase("Another cookies notice text", "Another cookies notice text")]
		public void GivenViewModelHasCookiesNoticeText_WhenIndexAction_ThenReturnViewodelWithCookiesNoticeText(string cookiesNoticeText, string expected)
		{
			//this._globalSettings.Setup(settings => settings.CookieNoticeText).Returns(cookiesNoticeText);
			var viewModel = ((CookiesNoticeViewModel)((PartialViewResult)this._sut.Index()).Model);

			Assert.AreEqual(expected, viewModel.CookieNoticeText.ToString());
		}
	}
}
