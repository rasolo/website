using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Tests.Unit.Base;
using Shouldly;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Tests.Unit.Shared.GlobalSettings
{
	internal class GlobalSettingsPageControllerTests : UmbracoBaseTests
	{
		private GlobalSettingsPageController _sut;
		private UmbracoMapper _umbracoMapper;
		private Mock<IUmbracoHelper> _umbracoHelperMock;
		private readonly GlobalSettingsPageViewModel _globalSettingsPageViewModel = new GlobalSettingsPageViewModel();

		public override void SetUp()
		{
			base.SetUp();
			this._umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._umbracoHelperMock = new Mock<IUmbracoHelper>();
			var content = this.SetupContent(DocumentTypeAlias.GlobalSettingsPage, this.SetupPropertyValue(GlobalSettingsPagePropertyAlias.HomeTextAlias, "Home text"));
			this._umbracoHelperMock.Setup(x => x.GlobalSettingsPage).Returns(content.Content);
		}

		[Test]
		public void Given_Controller_When_IndexAction_Then_GlobalSettingsViewModelFactoryIsCalled()
		{
			var globalSettingsViewModelFactoryMock = new Mock<IGlobalSettingsPageViewModelFactory>();
			this._sut = new GlobalSettingsPageController(globalSettingsViewModelFactoryMock.Object, _umbracoHelperMock.Object);
			globalSettingsViewModelFactoryMock.Setup(x => x.CreateModel(null)).Returns(this._globalSettingsPageViewModel);

			this._sut.Index();

			globalSettingsViewModelFactoryMock.Verify(x => x.CreateModel(null), Times.Exactly(1));
		}

		[Test]
		public void Given_Controller_When_IndexAction_Then_ReturnsGlobalSettingsViewModel()
		{
			this._sut = new GlobalSettingsPageController(new GlobalSettingsPagePageViewModelFactory(this._umbracoMapper, this._umbracoHelperMock.Object), this._umbracoHelperMock.Object);

			var returnedViewModel = (GlobalSettingsPageViewModel)((PartialViewResult)this._sut.Index()).Model;

			returnedViewModel.GetType().ShouldBe(this._globalSettingsPageViewModel.GetType());
		}
	}
}
