using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.StartPage;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests
{
	public class StartPageControllerTests
	{
		private Mock<IPublishedContent> content;
		private StartPageController sut;

		[SetUp]
		public void SetUp()
		{
			Current.Factory = Mock.Of<IFactory>();
			content = new Mock<IPublishedContent>();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this.sut = new StartPageController(umbracoMapper);
		}

		[TearDown]
		public void TearDown()
		{
			Current.Reset();
		}

		[Test]
		[TestCase("", null)]
		[TestCase(null, null)]
		[TestCase("Page title", "Page title")]
		[TestCase("Another Page title", "Another Page title")]
		public void GivenPageHasPageTitle_WhenIndexAction_ThenReturnViewModelWithPageTitle(string heading, string expected)
		{
			//TODO: Mock content.object, it is null.
			var viewModel = (StartPageViewModel)((ViewResult)this.sut.Index(new ContentModel(content.Object))).Model;

			Assert.AreEqual(expected, viewModel.Heading);
		}
	}
}