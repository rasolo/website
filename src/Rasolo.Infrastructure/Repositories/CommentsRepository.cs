using System;
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
	
		public IEnumerable<CommentViewModel> GetByContentId(int contentId)
		{
			var comments = this._commentsContext.Comments.Where(c => c.ContentId == contentId);
			return comments.Select(x => new CommentViewModel() {Message = x.Message, Name = x.Name, ContentId = contentId, DateCreated = x.DateCreated.ToString("yyyy-MM-dd")});
		}

		public void Add(CommentViewModel commentViewModel)
		{
			var comment = new Comment()
			{
				ContentId = commentViewModel.ContentId,
				DateCreated = DateTime.Now,
				Message = commentViewModel.Message,
				Name = commentViewModel.Name
			};

			this._commentsContext.Add(comment);
			this._commentsContext.SaveChanges();
		}
	}
}
