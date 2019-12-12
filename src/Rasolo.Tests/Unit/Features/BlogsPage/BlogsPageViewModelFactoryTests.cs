using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogsPage;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Features.BlogsPage
{
	internal class
		BlogsPageViewModelFactoryTests : BaseContentPageViewModelFactoryTests<Core.Features.BlogsPage.BlogsPage>
	{
		private BlogsPageViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			_sut = new BlogsPageViewModelFactory(umbracoMapper);
		}

		[Test]
		public void Given_PageHasBlogPages_When_CreateModel_Then_ReturnsBlogPages()
		{
			var blogPages = GetBlogPages();
			var propertyValue = SetupPropertyValue(BlogsPagePropertyAlias.BlogPages, blogPages);
			var content = SetupContent(DocumentTypeAlias.BlogsPage, propertyValue);
			var blogPage = _sut.CreateModel(content);

			blogPage.BlogPages.ShouldBe(blogPages);
			blogPage.BlogPages.Count().ShouldBe(blogPages.Count);
		}

		[Test]
		public void Given_PageHasZeroBlogPages_When_CreateModel_Then_ReturnsShowBlogPagesTrue()
		{
			var blogPages = GetBlogPages();
			var propertyValue = SetupPropertyValue(BlogsPagePropertyAlias.BlogPages, blogPages);
			var content = SetupContent(DocumentTypeAlias.BlogsPage, propertyValue);
			var blogPage = _sut.CreateModel(content);

			blogPage.ShowBlogPages.ShouldBe(true);
		}

		[Test]
		public void Given_PageHasZeroBlogPages_When_CreateModel_Then_ReturnsShowBlogPagesFalse()
		{
			var propertyValue = SetupPropertyValue(BlogsPagePropertyAlias.BlogPages, Enumerable.Empty<Core.Features.BlogPage.BlogPage>());
			var content = SetupContent(DocumentTypeAlias.BlogsPage, propertyValue);
			var blogPage = _sut.CreateModel(content);

			blogPage.ShowBlogPages.ShouldBe(false);
		}

		[Test]
		[TestCase("Teaser heading", "Teaser heading")]
		[TestCase("Another Teaser heading", "Another Teaser heading")]
		public void Given_PageHasTeaserHeading_When_IndexAction_Then_ReturnViewModelWithTeaserHeading(string teaserHeading, string expected)
		{
			var property = SetupPropertyValue(BlogsPagePropertyAlias.TeaserHeading, teaserHeading);
			var contentModel = SetupContent(nameof(BlogsPage), property);

			var viewModel = this._sut.CreateModel(contentModel);

			viewModel.TeaserHeading.ShouldBe(expected);
		}

		[Test]
		[TestCase("/media/hhon5crc/exception.png?anchor=center&mode=crop&width=500&height=500", "/media/hhon5crc/exception.png?anchor=center&mode=crop&width=500&height=500")]
		[TestCase("/media/ahen5cya/umbraco.png?anchor=center&mode=crop&width=500&height=500", "/media/ahen5cya/umbraco.png?anchor=center&mode=crop&width=500&height=500")]
		public void Given_PageHasTeaserUrl_When_IndexAction_Then_ReturnViewModelWithTeaserUrl(string teaserUrl, string expected)
		{
			var property = SetupPropertyValue(BlogsPagePropertyAlias.TeaserUrl, teaserUrl);
			var contentModel = SetupContent(nameof(BlogsPage), property);

			var viewModel = this._sut.CreateModel(contentModel);

			viewModel.TeaserUrl.ShouldBe(expected);
		}
	}
}