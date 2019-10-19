using System.Collections.Generic;
using NPoco;
using Rasolo.Core.Features.Shared.Compositions;

namespace Rasolo.Core.Features.SiteMapPage
{
	public class SiteMapPage : BaseContentPage
	{
		[Ignore]
		public IEnumerable<BaseContentPage> AllPages { get; set; }
	}
}