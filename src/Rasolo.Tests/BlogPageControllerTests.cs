using Rasolo.Core.Features.BlogPage;
using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Tests.Shared;

namespace Rasolo.Tests
{
	public class BlogPageControllerTests : BaseContentPageControllerTests<BlogPage>
	{
		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this._sut = new BlogPageController(umbracoMapper);
		}
	}
}
