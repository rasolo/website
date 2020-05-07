using Rasolo.Infrastructure.Entities;
using Umbraco.Core.Migrations;

namespace Rasolo.Infrastructure.Migrations
{
	/// <summary>
	///  creates a table in the DB if it's not already there
	/// </summary>
	public class CreateBlogPostCommentsTableMigration : MigrationBase
	{
		public CreateBlogPostCommentsTableMigration(IMigrationContext context) : base(context)
		{
		}

		public override void Migrate()
		{
			Logger.Debug(typeof(Comment), $"Running migration {nameof(CreateBlogPostCommentsTableMigration)}");

			// Lots of methods available in the MigrationBase class - discover with this.
			if (TableExists(Data.Constants.BlogPostCommentsTableName) == false)
			{
				Logger.Debug(typeof(Comment), $"Creating database table: {Data.Constants.BlogPostCommentsTableName}");
				Create.Table<Comment>().Do();
			}
			else
			{
				Logger.Debug(typeof(Comment), $"The database table {Data.Constants.BlogPostCommentsTableName} already exists, skipping");
			}
		}
	}
}


