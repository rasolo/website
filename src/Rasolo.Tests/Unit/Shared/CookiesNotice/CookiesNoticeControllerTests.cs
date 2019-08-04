using System.Web;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Tests.Unit.Base;

namespace Rasolo.Tests.Unit.Shared.CookiesNotice
{
	public class CookiesNoticeControllerTests : UmbracoBaseTests
	{
		private Mock<ICookiesNoticeViewModelFactory> _cookiesNoticeViewModelFactory;
		private CookiesNoticeController _sut;

		public override void SetUp()
		{
			base.SetUp();
			this._cookiesNoticeViewModelFactory = new Mock<ICookiesNoticeViewModelFactory>();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._sut = new CookiesNoticeController(umbracoMapper, this._cookiesNoticeViewModelFactory.Object);
		}

		[Test]
		public void Index_OnRun_CookiesNoticeViewModelFactoryIsCalled()
		{
			this._sut.Index();
			this._cookiesNoticeViewModelFactory.Verify(x => x.CreateModel(It.IsAny<HttpCookieCollection>()), Times.Exactly(1));
		}
	}
}