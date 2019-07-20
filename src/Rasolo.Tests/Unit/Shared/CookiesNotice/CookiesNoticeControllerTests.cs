using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.CookiesNotice;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
using System.Web.Mvc;

namespace Rasolo.Tests.Unit.Shared.CookiesNotice
{
	public class CookiesNoticeControllerTests : UmbracoBaseTests
	{
		private CookiesNoticeController _sut;

		public override void SetUp()
		{
			base.SetUp();
		}

		[Test]
		[TestCase("My cookies notice text", "My cookies notice text")]
		[TestCase("Another cookies notice text", "Another cookies notice text")]
		public void GivenViewModelHasCookiesNoticeText_WhenIndexAction_ThenReturnViewodelWithCookiesNoticeText(string cookiesNoticeText, string expected)
		{
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var property = SetupPropertyValue(GlobalSettingsPagePropertyAlias.CookieNoticeText, cookiesNoticeText);
			var content = base.SetupContent(DocumentTypeAlias.GlobalSettingsPage, property);
			umbracoServiceMock.Setup(x => x.GetFirstContentTypeAtRoot(It.IsAny<string>())).Returns(content.Content);
			var cookiesNoticeViewModelFactory = new Mock<CookiesNoticeViewModelFactory>(umbracoMapper, umbracoServiceMock.Object);

			this._sut = new CookiesNoticeController(umbracoMapper, cookiesNoticeViewModelFactory.Object);
			var viewModel = ((CookiesNoticeViewModel)((PartialViewResult)this._sut.Index()).Model);

			Assert.AreEqual(expected, viewModel.CookieNoticeText.ToString());
		}
	}
}
