using Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper;
using Rasolo.Core.Features.Shared.Compositions;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.ArticlePage
{
	public class ArticlePageViewModelFactory : BaseContentPageViewModelFactory<ArticlePage>, IArticlePageViewModelFactory
	{
		public ArticlePageViewModelFactory(IUmbracoMapper umbracoMapper, IUmbracoHelper umbracoHelper) : base(umbracoMapper, umbracoHelper)
		{
		}
	}
}