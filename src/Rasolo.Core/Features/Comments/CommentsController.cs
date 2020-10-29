using System.Web;
using System.Web.Mvc;
using Rasolo.Infrastructure.Models;
using Rasolo.Infrastructure.Repositories;
using reCAPTCHA.MVC;
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

		[HttpPost]
		[ValidateInput(false)]
		[ValidateAntiForgeryToken]
		[CaptchaValidator]
		public ActionResult SubmitComment(CommentViewModel comment,bool captchaValid)
		{
			if (ModelState.IsValid && captchaValid)
			{
				this._commentsRepository.Add(comment);
			}

			return Redirect(Request.UrlReferrer.ToString());
		}
	}
}