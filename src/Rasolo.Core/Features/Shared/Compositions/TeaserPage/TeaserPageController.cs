using Rasolo.Core.Features.Shared.Controllers;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.Shared.Compositions.TeaserPage
{
	public class TeaserPageController : BasePageController<TeaserPage>
	{
		public TeaserPageController(IUmbracoMapper umbracoMapper) : base(umbracoMapper)
		{
		}
	}
}