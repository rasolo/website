using System.Web;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Tests.Unit.Base;

namespace Rasolo.Tests.Unit.Shared.CookiesNotice
{
	public class CookiesNoticeControllerTests : UmbracoBaseTests
	{
		private Mock<IGlobalSettingsPageViewModelFactory> _GlobalSettingsPageViewModelFactory;
		private CookiesNoticeController _sut;

		public override void SetUp()
		{
			base.SetUp();
			this._GlobalSettingsPageViewModelFactory = new Mock<IGlobalSettingsPageViewModelFactory>();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._sut = new CookiesNoticeController(umbracoMapper, this._GlobalSettingsPageViewModelFactory.Object);
		}

		[Test]
		public void Index_OnRun_GlobalSettingsPageViewModelFactoryIsCalled()
		{
			this._sut.Index();
			this._GlobalSettingsPageViewModelFactory.Verify(x => x.CreateModel(It.IsAny<HttpCookieCollection>()), Times.Exactly(1));
		}
	}
}