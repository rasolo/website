using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
using System.Web.Mvc;

namespace Rasolo.Tests.Unit.Shared.GlobalSettings
{
	class GlobalSettingsControllerTests : UmbracoBaseTests
	{
		private Mock<IGlobalSettingsViewModelFactory> _globalSettingsViewModelFactory;
		private GlobalSettingsController _sut;

		public override void SetUp()
		{
			base.SetUp();
			this._globalSettingsViewModelFactory = new Mock<IGlobalSettingsViewModelFactory>();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var content = this.SetupContent(DocumentTypeAlias.GlobalSettingsPage, this.SetupPropertyValue("homeText", "Home text"));
			umbracoServiceMock.Setup(x => x.GetFirstContentTypeAtRoot(It.IsAny<string>())).Returns(content.Content);
			this._sut = new GlobalSettingsController(umbracoMapper, this._globalSettingsViewModelFactory.Object, umbracoServiceMock.Object);
		}

		[Test]
		public void Index_OnRun_GlobalSettingsViewModelFactoryIsCalled()
		{
			this._globalSettingsViewModelFactory.Setup(x=> x.CreateModel(It.IsAny<GlobalSettingsViewModel>())).Returns(new GlobalSettingsViewModel());
			this._sut.Index();
			this._globalSettingsViewModelFactory.Verify(x => x.CreateModel(It.IsAny<GlobalSettingsViewModel>()), Times.Exactly(1));
		}

		[Test]
		public void Index_OnRun_ThenReturnsGlobalSettingsViewModel()
		{
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var content = this.SetupContent(DocumentTypeAlias.GlobalSettingsPage, this.SetupPropertyValue("homeText", "Home text"));
			umbracoServiceMock.Setup(x => x.GetFirstContentTypeAtRoot(It.IsAny<string>())).Returns(content.Content);
			var sut = new GlobalSettingsController(umbracoMapper, new GlobalSettingsViewModelFactory(), umbracoServiceMock.Object);
			var returnedViewModel = (GlobalSettingsViewModel)((PartialViewResult)sut.Index()).Model;
			var testViewModel = new GlobalSettingsViewModel();
			Assert.AreEqual(testViewModel.GetType(), returnedViewModel.GetType());
		}
	}
}
