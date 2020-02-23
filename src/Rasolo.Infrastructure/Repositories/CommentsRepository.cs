using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rasolo.Infrastructure.Data;
using Rasolo.Infrastructure.Entities;
using Rasolo.Infrastructure.Models;

namespace Rasolo.Infrastructure.Repositories
{
	public class CommentsRepository : ICommentsRepository
	{
		private readonly CommentsContext _commentsContext = new CommentsContext(new DbContextOptions<CommentsContext>());
		public IEnumerable<Comment> GetComments()
		{
			var comments = new List<Comment>();

			using (var dbContext = new CommentsContext(new DbContextOptions<CommentsContext>()))
			{
				comments = dbContext.Comments.ToList();
			}

			return comments;
		}

		public IEnumerable<CommentViewModel> GetByContentId(int contentId)
		{
			var comments = this._commentsContext.Comments.Where(c => c.ContentId == contentId);
			return comments.Select(x => new CommentViewModel() {Message = x.Message, Name = x.Name});
		}
	}
}
