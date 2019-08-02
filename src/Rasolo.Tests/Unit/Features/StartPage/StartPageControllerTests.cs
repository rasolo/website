using Rasolo.Core.Features.Shared.Mappings;
using Rasolo.Core.Features.StartPage;
using Rasolo.Tests.Unit.Shared.BaseContentPage;

namespace Rasolo.Tests.Unit.Features.StartPage
{
	public class StartPageControllerTests : BaseContentPageControllerTests<Core.Features.StartPage.StartPage>
	{
		public override void SetUp()
		{
			base.SetUp();
			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
			this.Sut = new StartPageController(umbracoMapper);
		}
	}
}