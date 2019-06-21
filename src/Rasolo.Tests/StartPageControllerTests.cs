using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.StartPage;
using System.Web.Mvc;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests
{
	public class StartPageControllerTests
	{
		private Mock<IPublishedContent> _content;
		private StartPageController _sut;

		[SetUp]
		public void SetUp()
		{
			Current.Factory = Mock.Of<IFactory>();
			_content = new Mock<IPublishedContent>();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._sut = new StartPageController(umbracoMapper);
		}

		[TearDown]
		public void TearDown()
		{
			Current.Reset();
		}

		[Test]
		[TestCase("Page name", "Page name")]
		[TestCase("Another Page name", "Another Page name")]
		public void GivenPageHasName_WhenIndexAction_ThenReturnViewModelWithPageName(string name, string expected)
		{
			_content.SetupGet(x => x.Name).Returns(name);
			var publishedContentMock = new ContentModel(this._content.Object);

			var viewModel = (StartPage)((ViewResult)this._sut.Index(publishedContentMock)).Model;

			Assert.AreEqual(expected, viewModel.Name);
		}
	}
}