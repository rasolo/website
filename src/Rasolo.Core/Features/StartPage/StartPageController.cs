using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Cms.Core.Mapping;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPageController : BaseContentPageController<StartPage>
	{
		public StartPageController
			(IUmbracoMapper umbracoMapper, IStartPageViewModelFactory viewModelFactory) : base(umbracoMapper,
			viewModelFactory)
		{
		}
	}
}
