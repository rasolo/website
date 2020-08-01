using System.Collections.Generic;
using AutoMapper.Configuration.Annotations;
using Rasolo.Core.Features.Shared.Compositions;

namespace Rasolo.Core.Features.SearchPage
{
	public class SearchPage : BaseContentPage
	{
		[Ignore]
		public string Query { get; set; }
		[Ignore] public List<SearchResultItem> Results { get; set; }
		[Ignore] public long TotalItems { get; set; }
		[Ignore]
		public int NumberOfPages { get; set; }
		[Ignore]
		public bool ShowPagination { get; set; }
		[Ignore]
		public string PaginationSearchQuery { get; set; }
		[Ignore]
		public int CurrentPaginationPageNumber { get; set; }
		[Ignore]
		public bool ShowNextPagePaginationSymbol { get; set; }
		[Ignore]
		public bool ShowPreviousPagePaginationSymbol { get; set; }
		[Ignore]
		public string NextPaginationPageUrl { get; set; }
		[Ignore]
		public string PreviousPaginationPageUrl { get; set; }
		[Ignore]
		public bool ShowSearchResults { get; set; }
	}
}