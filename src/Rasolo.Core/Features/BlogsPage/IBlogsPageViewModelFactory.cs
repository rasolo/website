using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace Rasolo.Core.Features.BlogsPage
{
	public interface IBlogsPageViewModelFactory : IBaseContentPageViewModelFactory<BlogsPage>
	{
		BlogsPage CreateModel(ContentModel contentModel);

	}
}
