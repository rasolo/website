using System.Web.Mvc;
using AutoMapper;
using Rasolo.Core.Features.Shared.Controllers;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.StartPage
{
	public class StartPageController : BasePageController<StartPage>
	{

		public StartPageController(IUmbracoMapper umbracoMapper) : base(umbracoMapper)
		{
		}
	}
}