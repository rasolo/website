using Rasolo.Core.Features.Shared.Compositions;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.ArticlePage
{
	public class ArticlePageController: BaseContentPageController<ArticlePage>
	{
		public ArticlePageController(IUmbracoMapper umbracoMapper, IArticlePageViewModelFactory viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{

		}
	}
}