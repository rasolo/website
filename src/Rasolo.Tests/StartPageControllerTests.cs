using System;
using System.Linq;
using System.Reflection;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.StartPage;
using System.Web.Mvc;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
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

		[Test]
		[TestCase("Page title", "Page title")]
		[TestCase("Another Page title", "Another Page title")]
		public void GivenPageHasTitle_WhenIndexAction_ThenReturnViewModelWithPageTitle(string title, string expected)
		{
			var pageName = "startPage";
			var propertyAlias = "title";

			var property = new Mock<IPublishedProperty>();
			var content = new Mock<IPublishedContent>();
			property.Setup(x => x.Alias).Returns(propertyAlias);
			property.Setup(x => x.HasValue(It.IsAny<string>(),It.IsAny<string>())).Returns(title != null);
			property.Setup(x => x.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(title);
			content.Setup(x => x.ContentType).Returns(new PublishedContentType(1234, pageName, PublishedItemType.Content,
			Enumerable.Empty<string>(), Enumerable.Empty<PublishedPropertyType>(), ContentVariation.Nothing));
			content.Setup(c => c.GetProperty(It.Is<string>(x => x == propertyAlias))).Returns(property.Object);
			content.Setup(x => x.Name).Returns(pageName);
			content.Setup(x => x.Id).Returns(99);


			var publishedContentMock = new ContentModel(content.Object);
			var viewModel = (StartPage)((ViewResult)this._sut.Index(publishedContentMock)).Model;

			Assert.AreEqual(expected, viewModel.Title);
		}
	}
}