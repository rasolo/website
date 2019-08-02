using NUnit.Framework;
using Rasolo.Core.Features.Shared.Controllers;
using Rasolo.Tests.Unit.Base;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Shared.BaseContentPage
{
	public class BaseContentPageControllerTests<TContentPage> : UmbracoBaseTests where TContentPage : Core.Features.Shared.UI.BaseContentPage, new()
	{
		protected BasePageController<TContentPage> Sut;

		[Test]
		[TestCase("Page name", "Page name")]
		[TestCase("Another Page name", "Another Page name")]
		public void GivenPageHasName_WhenIndexAction_ThenReturnViewModelWithPageName(string name, string expected)
		{
			Content.SetupGet(x => x.Name).Returns(name);
			var publishedContentMock = new ContentModel(Content.Object);

			var viewModel = (TContentPage)((ViewResult)Sut.Index(publishedContentMock)).Model;

			Assert.AreEqual(expected, viewModel.Name);
		}

		[Test]
		[TestCase("Page title", "Page title")]
		[TestCase("Another Page title", "Another Page title")]
		public void GivenPageHasTitle_WhenIndexAction_ThenReturnViewModelWithPageTitle(string title, string expected)
		{
			var property = SetupPropertyValue("title", title);
			var contentModel = SetupContent(nameof(BaseContentPage), property);
			var viewModel = (TContentPage)((ViewResult)Sut.Index(contentModel)).Model;

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

		[Test]
		[TestCase("My preamble text", "My preamble text")]
		[TestCase("Another preamble text", "Another preamble text")]
		public void GivenPageHasTeaserPreamble_WhenIndexAction_ThenReturnViewModelWithTeaserPreamble(string teaserPreamble, string expected)
		{
			var property = SetupPropertyValue("teaserPreamble", teaserPreamble);
			var contentModel = SetupContent((nameof(BaseContentPage)), property);
			var viewModel = (TContentPage)((ViewResult)this.Sut.Index(contentModel)).Model;

			Assert.AreEqual(expected, viewModel.TeaserPreamble.ToString());
		}
	}
}