using NUnit.Framework;
using Rasolo.Core.Features.Shared.GlobalSettings;
using Rasolo.Tests.Unit.Base;

namespace Rasolo.Tests.Unit.Shared.GlobalSettings
{
	class GlobalSettingsViewModelFactoryTests : UmbracoBaseTests
	{
		private GlobalSettingsViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();

			this._sut = new GlobalSettingsViewModelFactory();
		}

		[Test]
		[TestCase("The home text", "The home text")]
		[TestCase("Another home text", "Another home text")]
		public void CreateModel_OnHomeTextGiven_ThenReturnViewModelWithHomeText(string homeText, string expected)
		{
			var viewModel = this._sut.CreateModel(new GlobalSettingsViewModel { HomeText = homeText});

			Assert.AreEqual(expected, viewModel.HomeText);
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void CreateModel_OnHomeTextNullOrEmpty_ThenReturnViewModelWithHomeTextEmptyString(string homeText, string expected)
		{
			var viewModel = this._sut.CreateModel(new GlobalSettingsViewModel { HomeText = homeText });

			Assert.AreEqual(expected, viewModel.HomeText);
		}
	}
}
