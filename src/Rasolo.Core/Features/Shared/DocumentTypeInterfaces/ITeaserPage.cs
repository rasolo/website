using System;

namespace Rasolo.Core.Features.Shared.DocumentTypeInterfaces
{
	public interface ITeaserPage
	{
		string TeaserHeading { get; set; }
		string TeaserUrl { get; set; }
		string Url { get; }
		string ParentUrl { get; }
		string ParentName { get; }
		DateTime CreateDate { get; }
	}
}