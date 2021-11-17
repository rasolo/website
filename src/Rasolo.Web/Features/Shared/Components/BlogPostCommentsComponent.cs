//using Umbraco.Core.Logging;
//using Umbraco.Core.Composing;
//using Umbraco.Core.Migrations;
//using Umbraco.Core.Migrations.Upgrade;
//using Umbraco.Core.Scoping;
//using Umbraco.Core.Services;
//using System.Configuration;

//namespace Rasolo.Web.Features.Shared.Components
//{
//	public class BlogPostCommentsComponent : IComponent
//	{
//		private IScopeProvider _scopeProvider;
//		private IMigrationBuilder _migrationBuilder;
//		private IKeyValueService _keyValueService;
//		private ILogger _logger;

//		public BlogPostCommentsComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
//		{
//			_scopeProvider = scopeProvider;
//			_migrationBuilder = migrationBuilder;
//			_keyValueService = keyValueService;
//			_logger = logger;
//		}

//		public void Initialize()
//		{
//			// Create a migration plan for a specific project/feature
//			// We can then track that latest migration state/step for this project/feature
//			var migrationPlan = new MigrationPlan(Infrastructure.Data.Constants.BlogPostCommentsTableName);

//			// This is the steps we need to take
//			// Each step in the migration adds a unique value
//			System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[Infrastructure.Data.Constants.UmbracoDatabaseConnectionStringName].ConnectionString);
//			var databaseName = builder.InitialCatalog;

//			migrationPlan.From(string.Empty)
//				.To<Infrastructure.Migrations.CreateBlogPostCommentsTableMigration>(databaseName);

//			// Go and upgrade our site (Will check if it needs to do the work or not)
//			// Based on the current/latest step
//			var upgrader = new Upgrader(migrationPlan);
//			upgrader.Execute(_scopeProvider, _migrationBuilder, _keyValueService, _logger);
//		}

//		public void Terminate()
//		{
//		}
//	}
//}