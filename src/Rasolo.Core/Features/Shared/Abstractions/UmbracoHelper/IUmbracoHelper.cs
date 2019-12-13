using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper
{
	public interface IUmbracoHelper
	{
		IEnumerable<IPublishedContent> ContentAtRoot();
	}
}
