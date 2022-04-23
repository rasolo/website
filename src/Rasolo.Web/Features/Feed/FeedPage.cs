using Microsoft.AspNetCore.Http;
using Rasolo.Web.Features.Shared.Compositions;

namespace Rasolo.Web.Features.Feed
{
	public class FeedPage : BaseContentPage
	{
		public HttpContext HttpContext { get; set; }
	}
}