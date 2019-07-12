using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogPostPage;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Tests.Unit.Shared;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Features.BlogPostPage
{
	public class BlogPostPageControllerTests : BaseContentPageControllerTests<Core.Features.BlogPostPage.BlogPostPage>
	{
		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this.Sut = new BlogPostPageController(umbracoMapper);
		}

		[Test]
		[TestCase("My preamble text", "My preamble text")]
		[TestCase("Another preamble text", "Another preamble text")]
		public void GivenPageHasPreamble_WhenIndexAction_ThenReturnViewModelWithPreamble(string preamble, string expected)
		{
			var property = base.SetupPropertyValue("preamble", preamble);
			var contentModel = base.SetupContent((nameof(Core.Features.BlogPostPage.BlogPostPage)), property);
			var viewModel = (Core.Features.BlogPostPage.BlogPostPage)((ViewResult)this.Sut.Index(contentModel)).Model;

			Assert.AreEqual(expected, viewModel.Preamble);
		}
	}
}
