using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Core.Features.StartPage;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;

namespace Rasolo.Tests.Unit.Features.StartPage
{
	internal class StartPageViewModelFactoryTests : BaseContentPageViewModelFactoryTests<Core.Features.StartPage.StartPage>
	{
		private StartPageViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var umbracoService = new Mock<IUmbracoService>();
			var blogPostService = new Mock<IBlogPostService>();
			var umbracoHelperMock = new Mock<IUmbracoHelper>();

			_sut = new StartPageViewModelFactory(umbracoMapper, umbracoService.Object, blogPostService.Object, umbracoHelperMock.Object);
		}

		[Test]
		[TestCase("This string has a \nline break", "This string has a <br />line break")]
		public void Given_PageHasTitleWithLineBreak_When_SetViewModelProperties_Then_ReturnsPageTitleWithLineBreak(
			string beforeReplace, string afterReplace)
		{
			var viewModel = new Core.Features.StartPage.StartPage() {Title = beforeReplace};
			this._sut.SetViewModelProperties(viewModel, null);

			viewModel.Title.ShouldBe(afterReplace);
		}
	}
}