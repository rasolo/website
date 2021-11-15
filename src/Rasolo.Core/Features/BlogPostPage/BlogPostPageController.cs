using Anaximapper;
using Rasolo.Core.Features.Shared.Compositions;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPageController : BaseContentPageController<BlogPostPage>
	{
		public BlogPostPageController(IPublishedContentMapper anaxiMapper, IBlogPostPageViewModelFactory viewModelFactory) : base(anaxiMapper, viewModelFactory)
		{
		}
	}
}