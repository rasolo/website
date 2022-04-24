//using Rasolo.Web.Features.Shared.Services.Authentication;
//using Rasolo.Services.Abstractions.HttpRequest;
//using Umbraco.Web.Security;

//namespace Rasolo.Services.Authentication
//{
//	public class AuthenticationService : IAuthenticationService
//	{
//		private readonly IHttpRequest _httpRequest;

//		public AuthenticationService(IHttpRequest httpRequest)
//		{
//			_httpRequest = httpRequest;
//		}

//		public bool UserAuthenticated
//		{
//			get
//			{
//				if (this._httpRequest.HttpContextWrapper == null)
//				{
//					return false;
//				}

//				if (_httpRequest.HttpContextWrapper.GetUmbracoAuthTicket() == null)
//				{
//					return false;
//				}

//				return _httpRequest.HttpContextWrapper.GetUmbracoAuthTicket().Identity.IsAuthenticated;
//			}
//		}
//	}
//}