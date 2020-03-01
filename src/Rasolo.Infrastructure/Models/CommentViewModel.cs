using System.ComponentModel.DataAnnotations;

namespace Rasolo.Infrastructure.Models
{
	public class CommentViewModel
	{
		[Required]
		public int ContentId { get; set; }

		[Required]
		public string Name { get; set; }
		[Required]
		public string Message { get; set; }
		public string DateCreated { get; set; }
		public bool IsAdmin { get; set; }
	}
}
