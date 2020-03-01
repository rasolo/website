using System.Linq;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogPage;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Features.BlogPage
{
	internal class BlogPageViewModelFactoryTests : BaseContentPageViewModelFactoryTests<Core.Features.BlogPage.BlogPage>
	{
		private BlogPageViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			var blogPostServiceMock = new Mock<BlogPostService>(umbracoMapper);
			var umbracoHelperMock = new Mock<IUmbracoHelper>();
			_sut = new BlogPageViewModelFactory(new UmbracoMapperComposer().SetupMapper(), blogPostServiceMock.Object, umbracoHelperMock.Object);
		}

		[Test]
		public void Given_CreateModelAndPageHasBlogPosts_When_ContentModelGiven_Then_ReturnViewModelWithBlogPosts()
		{
			var mockedBlogPostPages = SetUpContentPages(3, DocumentTypeAlias.BlogPostPage).ToList();
			var blogPostPagesProperty = SetupPropertyValue(PropertyTypeAlias.Children, mockedBlogPostPages);
			var blogPage = SetupContentMock(DocumentTypeAlias.BlogPage, blogPostPagesProperty);

			blogPage.Setup(x => x.Children).Returns(mockedBlogPostPages);

			var contentModel = new ContentModel(blogPage.Object);

			var viewModel = _sut.CreateModel(new Core.Features.BlogPage.BlogPage(), contentModel);

			viewModel.BlogPosts.Count.ShouldBe(3);
		}
	}
}