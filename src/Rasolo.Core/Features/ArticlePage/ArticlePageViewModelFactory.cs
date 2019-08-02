using System.Web;

namespace Rasolo.Core.Features.ArticlePage
{
	public class ArticlePageViewModelFactory
	{
		public ArticlePage CreateModel(ArticlePage articlePage)
		{
			articlePage.Title = articlePage.Title ?? string.Empty;
			articlePage.MainBody = articlePage.MainBody ?? new HtmlString(string.Empty);
			return articlePage;
		}
	}
}