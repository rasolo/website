using Rasolo.Core.Features.BlogPage;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Tests.Unit.Shared;

namespace Rasolo.Tests.Unit.Features.BlogPage
{
	public class BlogPageControllerTests : BaseContentPageControllerTests<Core.Features.BlogPage.BlogPage>
	{
		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this.Sut = new BlogPageController(umbracoMapper);
		}
	}
}