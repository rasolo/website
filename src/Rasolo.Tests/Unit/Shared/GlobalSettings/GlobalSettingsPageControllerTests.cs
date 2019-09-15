using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Tests.Unit.Base;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Zone.UmbracoMapper.V8;
using Shouldly;

namespace Rasolo.Tests.Unit.Shared.GlobalSettings
{
	internal class GlobalSettingsPageControllerTests : UmbracoBaseTests
	{
		private GlobalSettingsPageController _sut;
		private UmbracoMapper _umbracoMapper;
		private Mock<IUmbracoService> _umbracoServiceMock;
		private readonly GlobalSettingsPageViewModel _globalSettingsPageViewModel = new GlobalSettingsPageViewModel();

		public override void SetUp()
		{
			base.SetUp();
			this._umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._umbracoServiceMock = new Mock<IUmbracoService>();
			var content = this.SetupContent(DocumentTypeAlias.GlobalSettingsPage, this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.HomeTextAlias, "Home text"));
			this._umbracoServiceMock.Setup(x => x.GetFirstContentTypeAtRoot(It.IsAny<string>())).Returns(content.Content);
		}

		[Test]
		public void Given_Controller_When_IndexAction_Then_GlobalSettingsViewModelFactoryIsCalled()
		{
			var globalSettingsViewModelFactoryMock = new Mock<IGlobalSettingsPageViewModelFactory>();
			this._sut = new GlobalSettingsPageController(globalSettingsViewModelFactoryMock.Object);
			globalSettingsViewModelFactoryMock.Setup(x => x.CreateModel(null)).Returns(this._globalSettingsPageViewModel);

			this._sut.Index();

			globalSettingsViewModelFactoryMock.Verify(x => x.CreateModel(null), Times.Exactly(1));
		}

		[Test]
		public void Given_Controller_When_IndexAction_Then_ReturnsGlobalSettingsViewModel()
		{
			this._sut = new GlobalSettingsPageController(new GlobalSettingsPagePageViewModelFactory(this._umbracoMapper, this._umbracoServiceMock.Object));

			var returnedViewModel = (GlobalSettingsPageViewModel)((PartialViewResult)this._sut.Index()).Model;

			returnedViewModel.GetType().ShouldBe(this._globalSettingsPageViewModel.GetType());
		}
	}
}
