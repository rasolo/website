using Anaximapper;
using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Cms.Core.Mapping;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPageController : BaseContentPageController<StartPage>
	{
		public StartPageController
			(IPublishedContentMapper anaxiMapper, IStartPageViewModelFactory viewModelFactory) : base(anaxiMapper,
			viewModelFactory)
		{
		}
	}
}
