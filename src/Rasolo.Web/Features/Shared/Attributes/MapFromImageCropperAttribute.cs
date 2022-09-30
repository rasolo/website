using Anaximapper;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;

namespace Rasolo.Web.Features.Shared.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class MapFromImageCropperAttribute : Attribute, IMapFromAttribute
	{
		public string CropName { get; set; }

		public void SetPropertyValue<T>(object fromObject, PropertyInfo property, T model, MappingContext context)
		{
			var imageCropperValue = fromObject as ImageCropperValue;
			if (imageCropperValue == null)
			{
				return;
			}
			var crops = imageCropperValue.Crops.First();
			var sb = new StringBuilder();
			sb.Append(imageCropperValue.Src);
			sb.Append("?width=");
			sb.Append(crops.Width);
			sb.Append("&height=");
			sb.Append(crops.Height);
			if(imageCropperValue.FocalPoint != null)
			{
                sb.Append("&rxy=");
				sb.Append(imageCropperValue.FocalPoint.Top);
				sb.Append("%2c");
				sb.Append(imageCropperValue.FocalPoint.Left);
            }

			property.SetValue(model, sb.ToString());
		}
	}
}