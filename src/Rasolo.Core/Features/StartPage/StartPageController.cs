using Rasolo.Core.Features.Shared.Controllers;
using Rasolo.Core.Features.Shared.UI;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPageController : BaseContentPageController<StartPage>
	{
		public StartPageController(IUmbracoMapper umbracoMapper, IStartPageViewModelFactory viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
		}
	}
}