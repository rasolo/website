using System.Web;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Tests.Unit.Base;

namespace Rasolo.Tests.Unit.Shared.CookiesNotice
{
	internal class CookiesNoticeControllerTests : UmbracoBaseTests
	{
		private Mock<IGlobalSettingsPageViewModelFactory> _globalSettingsPageViewModelFactory;
		private CookiesNoticeController _sut;

		public override void SetUp()
		{
			base.SetUp();
			this._globalSettingsPageViewModelFactory = new Mock<IGlobalSettingsPageViewModelFactory>();
			this._sut = new CookiesNoticeController(this._globalSettingsPageViewModelFactory.Object);
		}

		[Test]
		public void Index_OnRun_GlobalSettingsPageViewModelFactoryIsCalled()
		{
			this._sut.Index();
			this._globalSettingsPageViewModelFactory.Verify(x => x.CreateModel(It.IsAny<HttpCookieCollection>()), Times.Exactly(1));
		}
	}
}