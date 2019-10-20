using System;
using System.Reflection;
using Umbraco.Core.PropertyEditors.ValueConverters;
using Zone.UmbracoMapper.V8;
using Zone.UmbracoMapper.V8.Attributes;

namespace Rasolo.Core.Features.Shared.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class MapFromImageCropperAttribute : Attribute, IMapFromAttribute
	{
		public string CropName { get; set; }

		public void SetPropertyValue<T>(object fromObject, PropertyInfo property, T model, IUmbracoMapper mapper)
		{
			var imageCropperValue = fromObject as ImageCropperValue;
			if (imageCropperValue == null)
			{
				return;
			}

			property.SetValue(model, imageCropperValue.Src + imageCropperValue.GetCropUrl(CropName));
		}
	}
}