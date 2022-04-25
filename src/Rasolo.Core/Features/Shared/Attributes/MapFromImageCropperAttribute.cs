//using Anaximapper;
//using System;
//using System.Reflection;
//using Umbraco.Cms.Core.PropertyEditors.ValueConverters;

//namespace Rasolo.Core.Features.Shared.Attributes
//{
//	[AttributeUsage(AttributeTargets.Property)]
//	public class MapFromImageCropperAttribute : Attribute, IMapFromAttribute
//	{
//		public string CropName { get; set; }

//		public void SetPropertyValue<T>(object fromObject, PropertyInfo property, T model, IPublishedContentMapper anaxiMapper)
//		{
//			var imageCropperValue = fromObject as ImageCropperValue;
//			if (imageCropperValue == null)
//			{
//				return;
//			}

//			property.SetValue(model, imageCropperValue.Src + imageCropperValue.GetCropUrl(CropName, Current.ImageUrlGenerator, useFocalPoint: true));
//		}
//	}
//}