namespace Rasolo.Core.Features.Shared.Abstractions
{
	public class HttpUtilityAdapter : IHttpUtility
	{
		public string UrlDecode(string value) => string.IsNullOrEmpty(value) ? null : System.Web.HttpUtility.UrlDecode(value);
	}
}