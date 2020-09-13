using System.Linq;
using Moq;
using NUnit.Framework;
using Rasolo.Core.Features.BlogPage;
using Rasolo.Core.Features.BlogPostPage;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Rasolo.Services.Constants;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Shouldly;
using Umbraco.Core.Mapping;
using Umbraco.Web.Models;
using UmbracoMapper = Zone.UmbracoMapper.V8.UmbracoMapper;

namespace Rasolo.Tests.Unit.Features.BlogPage
{
	internal class BlogPageViewModelFactoryTests : BaseContentPageViewModelFactoryTests<Core.Features.BlogPage.BlogPage>
	{
		private BlogPageViewModelFactory _sut;
		private Mock<IBlogPostPageViewModelFactory> _blogPostPageViewModelFactoryMock;
		private UmbracoMapper _umbracoMapper;

		public override void SetUp()
		{
			base.SetUp();
			this._umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._blogPostPageViewModelFactoryMock = new Mock<IBlogPostPageViewModelFactory>();
			var blogPostServiceMock = new BlogPostService(_umbracoMapper, this._blogPostPageViewModelFactoryMock.Object);
			var umbracoHelperMock = new Mock<IUmbracoHelper>();
			_sut = new BlogPageViewModelFactory(_umbracoMapper, blogPostServiceMock, umbracoHelperMock.Object);
		}

		[Test]
		public void Given_CreateModelAndPageHasBlogPosts_When_ContentModelGiven_Then_ReturnViewModelWithBlogPosts()
		{
			var mockedBlogPostPages = SetUpContentPages(3, DocumentTypeAlias.BlogPostPage).ToList();
			var blogPostPage = new Core.Features.BlogPostPage.BlogPostPage();
			this._umbracoMapper.Map(mockedBlogPostPages.First(), blogPostPage);
			var blogPostPagesProperty = SetupPropertyValue(PropertyTypeAlias.Children, mockedBlogPostPages);
			var blogPage = SetupContentMock(DocumentTypeAlias.BlogPage, blogPostPagesProperty);

			blogPage.Setup(x => x.Children).Returns(mockedBlogPostPages);
			this._blogPostPageViewModelFactoryMock.Setup(x =>
					x.CreateModel(It.IsAny<Core.Features.BlogPostPage.BlogPostPage>(), It.IsAny<ContentModel>()))
				.Returns(blogPostPage);

			var contentModel = new ContentModel(blogPage.Object);
			var viewModel = _sut.CreateModel(new Core.Features.BlogPage.BlogPage(), contentModel);

			viewModel.BlogPosts.Count.ShouldBe(3);
		}
	}
}