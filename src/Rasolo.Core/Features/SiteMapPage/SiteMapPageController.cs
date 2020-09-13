using System.Collections.Generic;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Core.Features.Shared.Constants;
using Rasolo.Core.Features.Shared.Services;
using Rasolo.Services.Constants;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.SiteMapPage
{
	public class SiteMapPageController : BaseContentPageController<SiteMapPage>
	{
		private readonly IUmbracoMapper _umbracoMapper;
		private readonly IUmbracoService _umbracoService;

		public SiteMapPageController(IUmbracoMapper umbracoMapper, IBaseContentPageViewModelFactory<Features.SiteMapPage.SiteMapPage> viewModelFactory, IUmbracoService umbracoService) : base(umbracoMapper, viewModelFactory)
		{
			_umbracoMapper = umbracoMapper;
			_umbracoService = umbracoService;
		}

		public override ActionResult Index(ContentModel model)
		{
			var viewModel = (SiteMapPage)((ViewResult)base.Index(model)).Model;


			viewModel.AllPages = new List<BaseContentPage>();
			this._umbracoMapper.MapCollection(
				_umbracoService.GetFirstPageByDocumentTypeAtRootLevelAndDescendants(DocumentTypeAlias.StartPage),(List<BaseContentPage>)viewModel.AllPages);

			return View(viewModel);
		}
	}
}