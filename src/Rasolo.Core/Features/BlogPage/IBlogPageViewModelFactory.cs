using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Web.Models;

namespace Rasolo.Core.Features.BlogPage
{
	public interface IBlogPageViewModelFactory: IBaseContentPageViewModelFactory<BlogPage>
	{
		 BlogPage CreateModel(ContentModel viewModel);
	}
}
