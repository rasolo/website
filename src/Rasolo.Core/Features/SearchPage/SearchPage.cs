using AutoMapper.Configuration.Annotations;
using Rasolo.Core.Features.Shared.Compositions;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchPage : BaseContentPage
	{
		[Ignore]
		public string Query { get; set; }
	}
}