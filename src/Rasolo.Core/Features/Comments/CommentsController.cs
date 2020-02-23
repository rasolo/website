using System.Web.Mvc;
using Rasolo.Infrastructure.Models;
using Rasolo.Infrastructure.Repositories;
using Umbraco.Web.Mvc;

namespace Rasolo.Core.Features.Comments
{
	public class CommentsController : SurfaceController
	{
		private readonly ICommentsRepository _commentsRepository;

		public CommentsController(ICommentsRepository commentsRepository)
		{
			_commentsRepository = commentsRepository;
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult SubmitComment(CommentViewModel comment)
		{
			if (ModelState.IsValid)
			{
				this._commentsRepository.Add(comment);
			}

			return Redirect(Request.UrlReferrer.ToString());
		}
	}
}