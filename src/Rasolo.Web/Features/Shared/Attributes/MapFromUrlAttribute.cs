using System;
using System.Reflection;
using Anaximapper;
using Microsoft.Extensions.DependencyInjection;
using Rasolo.Web.Features.Shared.Compositions;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Rasolo.Web.Features.Shared.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class MapFromUrlAttribute : Attribute, IMapFromAttribute
	{
		public void SetPropertyValue<T>(object fromObject, PropertyInfo property, T model, MappingContext context)
		{
			var baseContentModel = model as BaseContentPage;
			if (baseContentModel == null)
			{
				return;
			}

			var publishedContentQuery =
				context.HttpContext.RequestServices.GetRequiredService<IPublishedContentQuery>();
			var modelAsPublishedContent = publishedContentQuery.Content(baseContentModel.Id);
			property.SetValue(model, modelAsPublishedContent.Url());
		}
	}
}