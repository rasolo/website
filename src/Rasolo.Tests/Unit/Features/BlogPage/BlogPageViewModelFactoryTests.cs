using Moq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Composers;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Constants.PropertyTypeAlias;
using Rasolo.Tests.Unit.Shared.Compositions.BaseContentPage;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Tests.Unit.Features.BlogPage
{
	internal class BlogPageViewModelFactoryTests : BaseContentPageViewModelFactoryTests<Core.Features.BlogPage.BlogPage>
	{
		private Core.Features.BlogPage.BlogPageViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();
			this._sut = new Core.Features.BlogPage.BlogPageViewModelFactory(new UmbracoMapperComposer().SetupMapper());
		}

			[Test]
			[TestCase("The main body of first blog post", "The main body of first blog post", 
				"The main body of second blog post", "The main body of second blog post")]
			[TestCase("Another the main body of first blog post", "Another the main body of first blog post",
				"Another the body of second blog post", "Another the body of second blog post")]
		public void Given_CreateModelAndPageHasBlogPosts_When_ContentModelGiven_Then_ReturnViewModelWithBlogPosts(string mainBodyFirstBlogPost, string expectedFirstBlogPostMainBody, string mainBodySecondBlogPost, string expectedSecondBlogPostMainBody)
		{
			var blogPostProperty1 = this.SetupPropertyValue(BaseContentPagePropertyAlias.MainBody, new HtmlString(mainBodyFirstBlogPost));
			var blogPostPage1 = this.SetupContent(DocumentTypeAlias.BlogPostPage, blogPostProperty1);
			var blogPostProperty2 = this.SetupPropertyValue(BaseContentPagePropertyAlias.MainBody, new HtmlString(mainBodySecondBlogPost));
			var blogPostPage2 = this.SetupContent(DocumentTypeAlias.BlogPostPage, blogPostProperty2);

			var blogPageBlogPostChildren = new List<IPublishedContent>();
			blogPageBlogPostChildren.Add(blogPostPage1.Content);
			blogPageBlogPostChildren.Add(blogPostPage2.Content);
			var blogTestProperty = this.SetupPropertyValue("children", blogPageBlogPostChildren);
			var blogTest = this.SetupContentMock(DocumentTypeAlias.BlogPage, blogTestProperty);
			blogTest.Setup(x => x.Children).Returns(blogPageBlogPostChildren);

			var contentModel = new ContentModel(blogTest.Object);

			var viewModel = this._sut.CreateModel(contentModel);

			viewModel.BlogPosts.Count.ShouldBeGreaterThan(1);
			viewModel.BlogPosts.First().MainBody.ToString().ShouldBe(expectedFirstBlogPostMainBody);
			viewModel.BlogPosts.Skip(1).First().MainBody.ToString().ShouldBe(expectedSecondBlogPostMainBody);

		}
	}
}
