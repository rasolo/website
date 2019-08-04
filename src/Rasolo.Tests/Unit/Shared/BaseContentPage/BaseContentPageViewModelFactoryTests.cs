using NUnit.Framework;
using Rasolo.Core.Features.Shared.UI;
using Rasolo.Tests.Unit.Base;
using System.Web;

namespace Rasolo.Tests.Unit.Shared.BaseContentPage
{
	public class BaseContentPageViewModelFactoryTests <TModel> : UmbracoBaseTests where TModel : Core.Features.Shared.UI.BaseContentPage, new()
	{
		protected BaseContentPageViewModelFactory<Core.Features.Shared.UI.BaseContentPage> _sut;

		public override void SetUp()
		{
			base.SetUp();
			this._sut = new BaseContentPageViewModelFactory<Core.Features.Shared.UI.BaseContentPage>();
		}

		[Test]
		[TestCase(null, "")]
		[TestCase("", "")]
		public void CreateModel_OnTitleNullOrEmpty_ThenReturnViewModelWithTitleEmptyString(string title, string expected)
		{
			var contentPage = new TModel() { Title = title };
			var viewModel = this._sut.CreateModel(contentPage);

			Assert.AreEqual(expected, viewModel.Title);
		}

		[Test]
		[TestCase("The content page title", "The content page title")]
		[TestCase("Another content page title", "Another content page title")]
		public void CreateModel_OnTitleGiven_ThenReturnViewModelWithTitle(string title, string expected)
		{
			var contentPage = new TModel() { Title = title };
			var viewModel = this._sut.CreateModel(contentPage);

			Assert.AreEqual(viewModel.Title, expected);
		}

		[Test]
		[TestCase("The content page main body", "The content page main body")]
		[TestCase("Another content page main body", "Another content page main body")]
		public void CreateModel_OnMainBodyGiven_ThenReturnViewModelWithMainBody(string mainBody, string expected)
		{
			var contentPage = new TModel() { MainBody = new HtmlString(mainBody) };
			var viewModel = this._sut.CreateModel(contentPage);

			Assert.AreEqual(viewModel.MainBody.ToString(), expected);
		}

		[Test]
		public void CreateModel_OnMainBodyNullOrEmpty_ThenReturnViewModelWithMainBodyEmptyString()
		{
			var contentPage = new TModel() { MainBody = null };
			var viewModel = this._sut.CreateModel(contentPage);

			Assert.AreEqual(viewModel.MainBody.ToString(), string.Empty);
		}
	}
}
