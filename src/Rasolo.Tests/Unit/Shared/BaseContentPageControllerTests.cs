using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Controllers;
using Rasolo.Core.Features.Shared.UI;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Shared
{
	public class BaseContentPageControllerTests<TContentPage> where TContentPage : BaseContentPage, new()
	{
		protected Mock<IPublishedContent> Content;
		protected BasePageController<TContentPage> Sut;

		[SetUp]
		public virtual void SetUp()
		{
			Current.Factory = Mock.Of<IFactory>();
			Content = new Mock<IPublishedContent>();
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
			Content.SetupGet(x => x.Name).Returns(name);
			var publishedContentMock = new ContentModel(Content.Object);

			var viewModel = (TContentPage) ((ViewResult) Sut.Index(publishedContentMock)).Model;

			Assert.AreEqual(expected, viewModel.Name);
		}

		[Test]
		[TestCase("Page title", "Page title")]
		[TestCase("Another Page title", "Another Page title")]
		public void GivenPageHasTitle_WhenIndexAction_ThenReturnViewModelWithPageTitle(string title, string expected)
		{
			var property = SetupPropertyValue("title", title);
			var contentModel = SetupContent(nameof(BaseContentPage), property);
			var viewModel = (TContentPage) ((ViewResult) Sut.Index(contentModel)).Model;

			Assert.AreEqual(expected, viewModel.Title);
		}

		[Test]
		[TestCase("Main body", "Main body")]
		[TestCase("Another Main body", "Another Main body")]
		public void GivenPageHasMainBody_WhenIndexAction_ThenReturnViewModelWithMainBody(string mainBody, string expected)
		{
			var property = SetupPropertyValue("mainBody", mainBody);
			var contentModel = SetupContent(nameof(BaseContentPage), property);
			var viewModel = (TContentPage)((ViewResult)Sut.Index(contentModel)).Model;

			Assert.AreEqual(expected, viewModel.MainBody.ToString());
		}

		[Test]
		[TestCase("Teaser heading", "Teaser heading")]
		[TestCase("Another Teaser heading", "Another Teaser heading")]
		public void GivenPageHasTeaserHeading_WhenIndexAction_ThenReturnViewModelWithTeaserHeading(string teaserHeading, string expected)
		{
			var property = SetupPropertyValue("teaserHeading", teaserHeading);
			var contentModel = SetupContent(nameof(BaseContentPage), property);
			var viewModel = (TContentPage)((ViewResult)Sut.Index(contentModel)).Model;

			Assert.AreEqual(expected, viewModel.TeaserHeading);
		}

		public Mock<IPublishedProperty> SetupPropertyValue(string propertyAlias, string propertyValue)
		{
			var property = new Mock<IPublishedProperty>();
			property.Setup(x => x.Alias).Returns(propertyAlias);
			property.Setup(x => x.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(propertyValue != null);
			property.Setup(x => x.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(propertyValue);
			return property;
		}

		public ContentModel SetupContent(string contentTypeAlias, Mock<IPublishedProperty> publishedProperty)
		{
			var content = new Mock<IPublishedContent>();
			content.Setup(x => x.ContentType).Returns(new PublishedContentType(1234, contentTypeAlias,
				PublishedItemType.Content,
				Enumerable.Empty<string>(), Enumerable.Empty<PublishedPropertyType>(), ContentVariation.Nothing));
			content.Setup(c => c.GetProperty(It.Is<string>(x => x == publishedProperty.Object.Alias)))
				.Returns(publishedProperty.Object);
			content.Setup(x => x.Name).Returns(contentTypeAlias);
			content.Setup(x => x.Id).Returns(99);


			var contentModel = new ContentModel(content.Object);

			return contentModel;
		}
	}
}