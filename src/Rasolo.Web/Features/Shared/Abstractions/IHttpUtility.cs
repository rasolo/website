namespace Rasolo.Web.Features.Shared.Abstractions
{
	public interface IHttpUtility
	{
		string UrlDecode(string value);
		string UrlEncode(string value);
	}
}
