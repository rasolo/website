using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Core.Features.Shared.UI;
using Rasolo.Tests.Unit.Base;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Tests.Unit.Shared.BaseContentPage
{
	internal class BaseContentPageControllerTests<TContentPage> : UmbracoBaseTests where TContentPage : Core.Features.Shared.UI.BaseContentPage, new()
	{
		private BaseContentPageController<TContentPage> _sut;
		private Mock<IBaseContentPageViewModelFactory<TContentPage>> _viewModelFactory;
		private UmbracoMapper _umbracoMapper;
		private readonly TContentPage _mockedViewModel = new TContentPage();

		public override void SetUp()
		{
			base.SetUp();
			this._viewModelFactory = new Mock<IBaseContentPageViewModelFactory<TContentPage>>();
			this._viewModelFactory.Setup(x => x.CreateModel(It.IsAny<TContentPage>())).Returns(this._mockedViewModel);
			this._umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._sut = new BaseContentPageController<TContentPage>(this._umbracoMapper, _viewModelFactory.Object);
		}

		[Test]
		public void Index_OnRun_ViewModelFactoryIsCalled()
		{
			var property = this.SetupPropertyValue("whatever", "whatever");
			var content = this.SetupContent(DocumentTypeAlias.ArticlePage, property);

			this._sut.Index(content);

			this._viewModelFactory.Verify(x => x.CreateModel(It.IsAny<TContentPage>()), Times.Exactly(1));
		}

		[Test]
		public void Index_OnRun_ThenReturnsPageViewModel()
		{
			var umbracoServiceMock = new Mock<IUmbracoService>();
			var property = this.SetupPropertyValue("whatever", "whatever");
			var content = this.SetupContent(DocumentTypeAlias.ArticlePage, property);
			umbracoServiceMock.Setup(x => x.GetFirstContentTypeAtRoot(It.IsAny<string>())).Returns(content.Content);
			this._sut = new BaseContentPageController<TContentPage>(this._umbracoMapper, this._viewModelFactory.Object);

			var returnedViewModel = (TContentPage)((ViewResult)_sut.Index(content)).Model;

			Assert.AreEqual(this._mockedViewModel.GetType(), returnedViewModel.GetType());
		}

		[Test]
		[TestCase("Page name", "Page name")]
		[TestCase("Another Page name", "Another Page name")]
		public void GivenPageHasName_WhenIndexAction_ThenReturnViewModelWithPageName(string name, string expected)
		{
			Content.SetupGet(x => x.Name).Returns(name);
			this._mockedViewModel.Name = name;

			var viewModel = (TContentPage)((ViewResult)_sut.Index(new ContentModel(Content.Object))).Model;

			Assert.AreEqual(expected, viewModel.Name);
		}

		[Test]
		[TestCase("Page title", "Page title")]
		[TestCase("Another Page title", "Another Page title")]
		public void GivenPageHasTitle_WhenIndexAction_ThenReturnViewModelWithPageTitle(string title, string expected)
		{
			var property = SetupPropertyValue("title", title);
			var contentModel = SetupContent(nameof(BaseContentPage), property);
			this._mockedViewModel.Title = title;

			var viewModel = (TContentPage)((ViewResult)_sut.Index(contentModel)).Model;

			Assert.AreEqual(expected, viewModel.Title);
		}

		[Test]
		[TestCase("Main body", "Main body")]
		[TestCase("Another Main body", "Another Main body")]
		public void GivenPageHasMainBody_WhenIndexAction_ThenReturnViewModelWithMainBody(string mainBody, string expected)
		{
			var property = SetupPropertyValue("mainBody", mainBody);
			var contentModel = SetupContent(nameof(BaseContentPage), property);
			this._mockedViewModel.MainBody = new MvcHtmlString(mainBody);

			var viewModel = (TContentPage)((ViewResult)_sut.Index(contentModel)).Model;

			Assert.AreEqual(expected, viewModel.MainBody.ToString());
		}

		[Test]
		[TestCase("Teaser heading", "Teaser heading")]
		[TestCase("Another Teaser heading", "Another Teaser heading")]
		public void GivenPageHasTeaserHeading_WhenIndexAction_ThenReturnViewModelWithTeaserHeading(string teaserHeading, string expected)
		{
			var property = SetupPropertyValue("teaserHeading", teaserHeading);
			var contentModel = SetupContent(nameof(BaseContentPage), property);
			this._mockedViewModel.TeaserHeading = teaserHeading;

			var viewModel = (TContentPage)((ViewResult)_sut.Index(contentModel)).Model;

			Assert.AreEqual(expected, viewModel.TeaserHeading);
		}

		[Test]
		[TestCase("My preamble text", "My preamble text")]
		[TestCase("Another preamble text", "Another preamble text")]
		public void GivenPageHasTeaserPreamble_WhenIndexAction_ThenReturnViewModelWithTeaserPreamble(string teaserPreamble, string expected)
		{
			var property = SetupPropertyValue("teaserPreamble", teaserPreamble);
			var contentModel = SetupContent((nameof(BaseContentPage)), property);
			this._mockedViewModel.TeaserPreamble = new MvcHtmlString(teaserPreamble);

			var viewModel = (TContentPage)((ViewResult)this._sut.Index(contentModel)).Model;

			Assert.AreEqual(expected, viewModel.TeaserPreamble.ToString());
		}
	}
}