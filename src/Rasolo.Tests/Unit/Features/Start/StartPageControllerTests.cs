using Anaximapper;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Rasolo.Web.Features.Shared.Compositions;
using Rasolo.Web.Features.StartPage;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoNineDemoSite.Tests.Extensions;

namespace Rasolo.Tests.Unit.Features.Start
{
    [TestFixture]
    public class StartPageControllerTests
    {
        private StartPageController controller;
        public Mock<IStartPageViewModelFactory> viewModelFactory;


        [SetUp]
        public void SetUp()
        {
            this.viewModelFactory = new Mock<IStartPageViewModelFactory>();
            this.controller = new StartPageController(Mock.Of<IPublishedContentMapper>(), viewModelFactory.Object, Mock.Of<ILogger<RenderController>>(), Mock.Of<ICompositeViewEngine>(), Mock.Of<IUmbracoContextAccessor>());
        }

        [Test]
        public void Given_Controller_When_IndexAction_Then_ViewModelFactoryIsCalled()
        {
            var publishedContent = new Mock<IPublishedContent>();
            publishedContent.SetupPropertyValue("whatever", "whatever");
            var contentModel = new ContentModel(publishedContent.Object);

            var viewModel = new StartPage();
            viewModelFactory.Setup(x => x.CreateModel(It.IsAny<StartPage>(), null)).Returns(viewModel);
            this.controller.StartPage(contentModel);

            viewModelFactory.Verify(x => x.CreateModel(It.IsAny<StartPage>(), contentModel), Times.Exactly(1));
        }
    }
}
