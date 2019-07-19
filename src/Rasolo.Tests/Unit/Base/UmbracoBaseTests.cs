using System.Linq;
using Moq;
using NUnit.Framework;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Base
{
	public abstract class UmbracoBaseTests
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
