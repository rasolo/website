using NPoco;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rasolo.Infrastructure.Entities
{
	[Table(Data.Constants.BlogPostCommentsTableName)]
	[TableName(Data.Constants.BlogPostCommentsTableName)]
	[PrimaryKey("Id")]
	public class Comment
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Message { get; set; }
		[Required]
		public int ContentId { get; set; }
		[Required]
		public DateTime DateCreated { get; set; }

		public bool IsAdmin { get; set; }
	}
}