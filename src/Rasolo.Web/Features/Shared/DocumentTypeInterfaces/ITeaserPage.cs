using System;

namespace Rasolo.Web.Features.Shared.DocumentTypeInterfaces
{
	public interface ITeaserPage
	{
		string TeaserHeading { get; set; }
		string TeaserUrl { get; set; }
		string ParentUrl { get; }
		string ParentName { get; }
		DateTime CreateDate { get; }
		string Url { get; set; }
	}
}