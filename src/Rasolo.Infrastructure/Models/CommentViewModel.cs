using System.ComponentModel.DataAnnotations;

namespace Rasolo.Infrastructure.Models
{
	public class CommentViewModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Message { get; set; }
	}
}
