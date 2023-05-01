using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Rasolo.Web.Features.SearchPage.Examine
{
		public class ExamineComposer : IComposer
		{
			public void Compose(IUmbracoBuilder builder)
			{
				builder.Services.ConfigureOptions<ConfigureExternalIndexOptions>();
			}
		}
}
