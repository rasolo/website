//using Rasolo.Core.Features.Shared.Settings;
//using System.Linq;
//using Umbraco.Core;
//using Umbraco.Core.Composing;
//using Umbraco.Web;
//using Umbraco.Web.Mvc;
//using Zone.UmbracoMapper.V8;

//namespace Rasolo.Core.Features.Shared.Mappings
//{
//	public class GlobalSettingsModelComposer : IUserComposer
//	{

//		public GlobalSettingsModel SetupModel()
//		{
//			var umbracoContext = new UmbracoContext();
//			var globalSettingsPage = Umbraco.Web.Composing.Current.UmbracoHelper.ContentAtRoot().FirstOrDefault(x => x.IsDocumentType("globalSettingsPage"));
//			var umbracoMapper = new UmbracoMapperComposer().SetupMapper();
//			var globalSettingsPageModel = new GlobalSettingsModel();
//			umbracoMapper.Map(globalSettingsPage, globalSettingsPageModel);

//			return globalSettingsPageModel;
//			//using (var cref = _umbracoContextFactory.EnsureUmbracoContext())
//			//{
//			//	var cache = cref.UmbracoContext.Content;
//			//	var globalSettingsPage = cache.GetAtRoot().FirstOrDefault(x => x.IsDocumentType("globalSettingsPage"));
//			//	var globalSettingsPageModel = new GlobalSettingsModel();
//			//	_mapper.Map(globalSettingsPage, globalSettingsPageModel);

//			//	return globalSettingsPageModel;
//			//}

//			//return new GlobalSettingsModel();
//		}

//		public void Compose(Composition composition)
//		{
//			var model = this.SetupModel();
//			composition.Register<IGlobalSettings>(model);
//		}
//	}
//}