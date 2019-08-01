using Rasolo.Core.Features.BlogPostPage;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Tests.Unit.Shared;

namespace Rasolo.Tests.Unit.Features.BlogPostPage
{
	public class BlogPostPageControllerTests : BaseContentPageControllerTests<Core.Features.BlogPostPage.BlogPostPage>
	{
		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this.Sut = new BlogPostPageController(umbracoMapper);
		}
	}
}
