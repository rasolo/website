using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Rasolo.Infrastructure.Entities;

namespace Rasolo.Infrastructure.Data
{
	public class CommentsContext : DbContext
	{
		public DbSet<Comment> Comments { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			{
				if (optionsBuilder.IsConfigured == false)
				{
					optionsBuilder.UseSqlServer(
						ConfigurationManager.ConnectionStrings[Constants.UmbracoDatabaseConnectionStringName].ConnectionString);
				}

				base.OnConfiguring(optionsBuilder);
			}
		}
		public CommentsContext(DbContextOptions<CommentsContext> options)
			: base(options) { }
	}
}