using System.Linq;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogPage;
using Rasolo.Core.Features.BlogPostPage;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Infrastructure.Repositories;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Features.BlogPostPage
{
	internal class BlogPostPageViewModelFactoryTests : BaseContentPageViewModelFactoryTests<Core.Features.BlogPostPage.BlogPostPage>
	{
		private BlogPostPageViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();
			_sut = new BlogPostPageViewModelFactory(new UmbracoMapperComposer().SetupMapper(), new Mock<IUmbracoHelper>().Object,
				new Mock<ICommentsRepository>().Object);
		}

		[Test]
		[TestCase("This is the alternative text", "This is the alternative text")]
		[TestCase(null, null)]
		public void Given_CreateModelAndPageHasTeaserMediaAltText_When_ContentModelGiven_Then_ReturnViewModelTeaserMediaAltText(string teaserMediaAltText, string expected)
		{
			var teaserMediaAltTextProperty = SetupPropertyValue(PropertyTypeAlias.TeaserMediaAltText, teaserMediaAltText);
			var blogPostPage = SetupContentMock(DocumentTypeAlias.BlogPostPage, teaserMediaAltTextProperty);

			blogPostPage.Setup(x => x.GetProperty(teaserMediaAltText)).Returns(teaserMediaAltTextProperty.Object);


			var contentModel = new ContentModel(blogPostPage.Object);

			var viewModel = _sut.CreateModel(null, contentModel);

			viewModel.TeaserMediaAltText.ShouldBe(expected);
		}

		[Test]
		[TestCase("This is the alternative text", true)]
		public void Given_CreateModelAndPageHasTeaserMediaAltText_When_ContentModelGiven_Then_ReturnViewModelShowTeaserMediaAltTextAsTrue(string teaserMediaAltText, bool expected)
		{
			var teaserMediaAltTextProperty = SetupPropertyValue(PropertyTypeAlias.TeaserMediaAltText, teaserMediaAltText);
			var blogPostPage = SetupContentMock(DocumentTypeAlias.BlogPostPage, teaserMediaAltTextProperty);

			blogPostPage.Setup(x => x.GetProperty(teaserMediaAltText)).Returns(teaserMediaAltTextProperty.Object);


			var contentModel = new ContentModel(blogPostPage.Object);

			var viewModel = _sut.CreateModel(null, contentModel);

			viewModel.ShowTeaserMediaAltText.ShouldBe(expected);
		}

		[Test]
		[TestCase("", false)]
		[TestCase(null, false)]
		public void Given_CreateModelAndPageTeaserMediaAltTextNullOrEmpty_When_ContentModelGiven_Then_ReturnViewModelShowTeaserMediaAltTextAsFalse(string teaserMediaAltText, bool expected)
		{
			var teaserMediaAltTextProperty = SetupPropertyValue(PropertyTypeAlias.TeaserMediaAltText, teaserMediaAltText);
			var blogPostPage = SetupContentMock(DocumentTypeAlias.BlogPostPage, teaserMediaAltTextProperty);

			blogPostPage.Setup(x => x.GetProperty(teaserMediaAltText)).Returns(teaserMediaAltTextProperty.Object);


			var contentModel = new ContentModel(blogPostPage.Object);

			var viewModel = _sut.CreateModel(null, contentModel);

			viewModel.ShowTeaserMediaAltText.ShouldBe(expected);
		}
	}
}