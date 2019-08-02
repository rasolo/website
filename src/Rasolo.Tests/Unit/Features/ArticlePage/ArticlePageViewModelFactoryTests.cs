using NUnit.Framework;
using Rasolo.Core.Features.ArticlePage;
using Rasolo.Tests.Unit.Base;
using System.Web;

namespace Rasolo.Tests.Unit.Features.ArticlePage
{
	class ArticlePageViewModelFactoryTests : UmbracoBaseTests
	{
		private ArticlePageViewModelFactory _sut;

		public override void SetUp()
		{
			base.SetUp();
			this._sut = new ArticlePageViewModelFactory();
		}

		//TODO: Put these tests in base?

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void CreateModel_OnTitleNullOrEmpty_ThenReturnViewModelWithTitleEmptyString(string title, string expected)
		{
			var articlePage = new Rasolo.Core.Features.ArticlePage.ArticlePage() { Title = title};
			var viewModel = this._sut.CreateModel(articlePage);

			Assert.AreEqual(viewModel.Title, expected);
		}

		[Test]
		[TestCase("The article title", "The article title")]
		[TestCase("Another article title", "Another article title")]
		public void CreateModel_OnTitleGiven_ThenReturnViewModelWithTitle(string title, string expected)
		{
			var articlePage = new Rasolo.Core.Features.ArticlePage.ArticlePage() { Title = title };
			var viewModel = this._sut.CreateModel(articlePage);

			Assert.AreEqual(viewModel.Title, expected);
		}

		[Test]
		[TestCase("The article main body", "The article main body")]
		[TestCase("Another article main body", "Another article main body")]
		public void CreateModel_OnMainBodyGiven_ThenReturnViewModelWithMainBody(string mainBody, string expected)
		{
			var articlePage = new Rasolo.Core.Features.ArticlePage.ArticlePage() { MainBody = new HtmlString(mainBody)};
			var viewModel = this._sut.CreateModel(articlePage);

			Assert.AreEqual(viewModel.MainBody.ToString(), expected);
		}

		[Test]
		public void CreateModel_OnMainBodyNullOrEmpty_ThenReturnViewModelWithMainBodyEmptyString()
		{
			var articlePage = new Rasolo.Core.Features.ArticlePage.ArticlePage() { MainBody = null };
			var viewModel = this._sut.CreateModel(articlePage);

			Assert.AreEqual(viewModel.MainBody.ToString(), string.Empty);
		}
	}
}
