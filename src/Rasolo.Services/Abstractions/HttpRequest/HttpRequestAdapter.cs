//using Microsoft.AspNetCore.Http;
//using System.Collections.Specialized;
//using System.Web;

//namespace Rasolo.Services.Abstractions.HttpRequest
//{
//	public class HttpRequestAdapter : IHttpRequest
//	{
//		public HttpContextWrapper HttpContextWrapper => new HttpContextWrapper(HttpContext.Current);
//		public NameValueCollection QueryString => HttpContext.Current?.Request?.QueryString;
//	}
//}