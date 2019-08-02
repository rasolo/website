using Rasolo.Core.Features.ArticlePage;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Tests.Unit.Shared;

namespace Rasolo.Tests.Unit.Features.ArticlePage
{
	class ArticlePageControllerTests : BaseContentPageControllerTests<Core.Features.ArticlePage.ArticlePage>
	{
		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this.Sut = new ArticlePageController(umbracoMapper);
		}
	}
}
