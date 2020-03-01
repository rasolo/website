using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Rasolo.Core.Features.Shared.Services.Authentication;
using Rasolo.Infrastructure.Data;
using Rasolo.Infrastructure.Entities;
using Rasolo.Infrastructure.Models;
using Rasolo.Services.Mail;

namespace Rasolo.Infrastructure.Repositories
{
	public class CommentsRepository : ICommentsRepository
	{
		private readonly IAuthenticationService _authenticationService;
		private readonly IMailService _mailService;

		public CommentsRepository(IAuthenticationService authenticationService, IMailService mailService)
		{
			_authenticationService = authenticationService;
			_mailService = mailService;
		}
		private readonly CommentsContext _commentsContext = new CommentsContext(new DbContextOptions<CommentsContext>());
	
		public IEnumerable<CommentViewModel> GetByContentId(int contentId)
		{
			var comments = this._commentsContext.Comments.Where(c => c.ContentId == contentId);
			return comments.Select(x => new CommentViewModel() {Message = x.Message, Name = x.Name, ContentId = contentId, DateCreated = x.DateCreated.ToString("yyyy-MM-dd"), IsAdmin = x.IsAdmin});
		}

		public void Add(CommentViewModel commentViewModel)
		{
			var comment = new Comment()
			{
				ContentId = commentViewModel.ContentId,
				DateCreated = DateTime.Now,
				Message = HttpUtility.HtmlEncode(commentViewModel.Message),
				Name = HttpUtility.HtmlEncode(commentViewModel.Name),
				IsAdmin = this._authenticationService.UserAuthenticated
			};

			this._commentsContext.Add(comment);
			this._commentsContext.SaveChanges();

			SendNewCommentEmail(comment);
		}

		private void SendNewCommentEmail(Comment comment)
		{
			var plainMessage =
				$"From: {comment.Name} ContentId: {comment.ContentId} Is admin: {comment.IsAdmin} Date: {comment.DateCreated} Message: {comment.Message}";
			var htmlMessage =
				$"From: {comment.Name} <br> ContentId: {comment.ContentId} <br> Is admin: {comment.IsAdmin} <br> Date: {comment.DateCreated} <br> Message: {comment.Message}";

			this._mailService.SendMail(plainMessage, htmlMessage);
		}
	}
}
