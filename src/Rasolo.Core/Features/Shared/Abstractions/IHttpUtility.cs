namespace Rasolo.Core.Features.Shared.Abstractions
{
	public interface IHttpUtility
	{
		string UrlDecode(string value);
		string UrlEncode(string value);
	}
}
