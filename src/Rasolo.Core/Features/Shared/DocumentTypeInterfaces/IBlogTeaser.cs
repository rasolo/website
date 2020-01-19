using System.Collections.Generic;

namespace Rasolo.Core.Features.Shared.DocumentTypeInterfaces
{
	public interface IBlogTeaser
	{
		 bool ShowPosts { get; }
		IEnumerable<ITeaserPage> Posts { get; }
	}
}
