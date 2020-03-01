using System.Collections.Generic;
using Rasolo.Infrastructure.Models;

namespace Rasolo.Infrastructure.Repositories
{
	public interface ICommentsRepository
	{
		IEnumerable<CommentViewModel> GetByContentId(int contentId);
		void Add(CommentViewModel comment);
	}
}
