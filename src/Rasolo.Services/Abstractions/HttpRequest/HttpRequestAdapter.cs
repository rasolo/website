using System.Web;

namespace Rasolo.Services.Abstractions.HttpRequest
{
	public class HttpRequestAdapter : IHttpRequest
	{
		public HttpContextWrapper HttpContextWrapper => new HttpContextWrapper(HttpContext.Current);
	}
}