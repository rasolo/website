using Microsoft.AspNetCore.Mvc;

namespace Rasolo.Web.Features.SearchPage
{
	[ViewComponent(Name = "SearchPageForm")]
	public class SearchPageFormComponent : ViewComponent
	{
		public IViewComponentResult Invoke(SearchPageViewModel model)
		{
			return View(model);
		}
	}
}