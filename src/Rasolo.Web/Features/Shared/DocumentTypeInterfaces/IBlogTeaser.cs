using System.Collections.Generic;

namespace Rasolo.Web.Features.Shared.DocumentTypeInterfaces
{
	public interface IBlogTeaser
	{
		bool ShowPosts { get; }
		IEnumerable<ITeaserPage> Posts { get; }
		string PostsTitle { get; set; }
	}
}