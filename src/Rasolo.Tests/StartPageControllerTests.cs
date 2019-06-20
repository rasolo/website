using Moq;
using NUnit.Framework;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Tests {
    public class StartPageControllerTests
    {
        private Mock<IPublishedContent> content;

        [SetUp]
        public void SetUp() {
            Current.Factory = Mock.Of<IFactory>();
            this.content = new Mock<IPublishedContent>();
        }

    }
}
