using Anaximapper;
using Rasolo.Core.Features.Shared.Compositions;

namespace Rasolo.Core.Features.BlogsPage
{
	public class BlogsPageController : BaseContentPageController<BlogsPage>
	{
		public BlogsPageController(IPublishedContentMapper anaxiMapper,
			IBlogsPageViewModelFactory viewModelFactory) : base(anaxiMapper, viewModelFactory)
		{
		}
	}
}