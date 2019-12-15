using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogPage;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Base
{
	internal abstract class UmbracoBaseTests
	{
		protected Mock<IPublishedContent> Content;

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

		public Mock<IPublishedProperty> SetupPropertyValue<T>(string propertyAlias, T propertyValue)
		{
			var property = new Mock<IPublishedProperty>();
			property.Setup(x => x.Alias).Returns(propertyAlias);
			property.Setup(x => x.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(propertyValue != null);
			property.Setup(x => x.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(propertyValue);
			return property;
		}

		public ContentModel SetupContent(string contentTypeAlias, Mock<IPublishedProperty> publishedProperty, string url = "http://rasolo.local/pagename", string pageName = "anyPageName")
		{
			var content = SetupContentMock(contentTypeAlias, publishedProperty, url, pageName);

			var contentModel = new ContentModel(content.Object);

			return contentModel;
		}

		public Mock<IPublishedContent> SetupContentMock(string contentTypeAlias, Mock<IPublishedProperty> publishedProperty, string url = "http://rasolo.local/pagename", string pageName = "anyPageName")
		{
			var content = new Mock<IPublishedContent>();
			content
				.Setup(x => x.ContentType)
				.Returns(new PublishedContentType(1234, contentTypeAlias, PublishedItemType.Content,
				Enumerable.Empty<string>(), Enumerable.Empty<PublishedPropertyType>(), ContentVariation.Nothing));
			content.Setup(c => c.GetProperty(It.Is<string>(x => x == publishedProperty.Object.Alias)))
				.Returns(publishedProperty.Object);
			content.Setup(x => x.Id).Returns(99);
			content.Setup(x => x.Url).Returns(url);
			content.Setup(x => x.Name).Returns(pageName);

			return content;
		}

		public static Mock<IPublishedContent> SetupImage()
		{
			var widthPropertyMock = new Mock<IPublishedProperty>();
			widthPropertyMock.Setup(c => c.Alias).Returns("umbracoWidth");
			widthPropertyMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			widthPropertyMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(100);

			var heightPropertyMock = new Mock<IPublishedProperty>();
			heightPropertyMock.Setup(c => c.Alias).Returns("umbracoHeight");
			heightPropertyMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			heightPropertyMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(200);

			var sizePropertyMock = new Mock<IPublishedProperty>();
			sizePropertyMock.Setup(c => c.Alias).Returns("umbracoBytes");
			sizePropertyMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			sizePropertyMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(1000);

			var extensionPropertyMock = new Mock<IPublishedProperty>();
			extensionPropertyMock.Setup(c => c.Alias).Returns("umbracoExtension");
			extensionPropertyMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			extensionPropertyMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns(".jpg");

			var altTextPropertyMock = new Mock<IPublishedProperty>();
			altTextPropertyMock.Setup(c => c.Alias).Returns("altText");
			altTextPropertyMock.Setup(c => c.HasValue(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
			altTextPropertyMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<string>())).Returns("Test image alt text");

			var contentType = new PublishedContentType(1000, "Image", PublishedItemType.Media, Enumerable.Empty<string>(), Enumerable.Empty<PublishedPropertyType>(), ContentVariation.Nothing);

			var imageMock = new Mock<IPublishedContent>();
			imageMock.Setup(c => c.Id).Returns(2000);
			imageMock.Setup(c => c.Name).Returns("Test image");
			imageMock.Setup(c => c.Url).Returns("/media/test.jpg");
			imageMock.Setup(c => c.ContentType).Returns(contentType);
			imageMock.Setup(c => c.GetProperty(It.Is<string>(x => x == "umbracoWidth"))).Returns(widthPropertyMock.Object);
			imageMock.Setup(c => c.GetProperty(It.Is<string>(x => x == "umbracoHeight"))).Returns(heightPropertyMock.Object);
			imageMock.Setup(c => c.GetProperty(It.Is<string>(x => x == "umbracoBytes"))).Returns(sizePropertyMock.Object);
			imageMock.Setup(c => c.GetProperty(It.Is<string>(x => x == "umbracoExtension"))).Returns(extensionPropertyMock.Object);
			imageMock.Setup(c => c.GetProperty(It.Is<string>(x => x == "altText"))).Returns(altTextPropertyMock.Object);
			return imageMock;
		}

		public List<BlogPage> GetBlogPages()
		{
			return new List<Core.Features.BlogPage.BlogPage>
			{
				new BlogPage()
				{
					Name = "Umbraco",
					Url = "http://rasolo.azurewebsites.net/blogs/umbraco",
					Title = "Umbraco blog"
				},
				new BlogPage()
				{
					Name = "Episerver",
					Url = "http://rasolo.azurewebsites.net/blogs/episerver",
					Title = "Episerver blog"
				}
			};
		}

	}
}
