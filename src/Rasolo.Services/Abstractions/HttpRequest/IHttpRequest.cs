using System.Web;

namespace Rasolo.Services.Abstractions.HttpRequest
{
	public interface IHttpRequest
	{
		HttpContextWrapper HttpContextWrapper { get; }
	}
}
