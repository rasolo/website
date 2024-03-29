using System;
using Anaximapper.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rasolo.Web.Features.BlogPostPage;
using Rasolo.Web.Features.BlogsPage;
using Rasolo.Web.Features.Shared.Abstractions;
using Rasolo.Web.Features.Shared.Services;
using Rasolo.Web.Features.StartPage;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Rasolo.Web.Features.BlogPage;
using Rasolo.Web.Features.Feed;
using Rasolo.Web.Features.SearchPage;
using Rasolo.Web.Features.SearchPage.Examine;
using Rasolo.Web.Features.Shared;
using Rasolo.Web.Features.Shared.GlobalSettings;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

namespace Rasolo.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="webHostEnvironment">The web hosting environment.</param>
        /// <param name="config">The configuration.</param>
        /// <remarks>
        /// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337
        /// </remarks>
        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
			if (!_env.IsDevelopment())
			{
				services.AddUmbraco(_env, _config)
					.AddBackOffice()
					.AddWebsite()
					.AddComposers()
					.AddAnaximapper()
					.AddAzureBlobMediaFileSystem()
					.Build();
			}
			else
			{
				services.AddUmbraco(_env, _config)
					.AddBackOffice()
					.AddWebsite()
					.AddComposers()
					.AddAnaximapper()
					.Build();
			}

			services.AddScoped<IUmbracoHelper>(sp => new UmbracoHelperAdapter(sp.GetRequiredService<UmbracoHelper>()));
			services.AddScoped<IBlogPostPageViewModelFactory, BlogPostPageViewModelFactory>();
			services.AddScoped<IBlogsPageViewModelFactory, BlogsPageViewModelFactory>();
			services.AddScoped<IBlogPageViewModelFactory, BlogPageViewModelFactory>();
			services.AddScoped<IHttpUtility, HttpUtilityAdapter>();
			services.AddScoped<IBlogPostService, BlogPostService>();
			services.AddScoped<IUmbracoService, UmbracoService>();
			services.AddScoped<IFeedPageViewModelFactory<FeedPage>, FeedPageViewModelFactory>();

			services.AddScoped<IGlobalSettingsPageViewModelFactory, GlobalSettingsPagePageViewModelFactory>();
			services.AddScoped<ISearchPageViewModelFactory, SearchPageViewModelFactory>();
			services.AddSingleton<IExamineSearcher, ExamineSearcher>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IStartPageViewModelFactory, StartPageViewModelFactory>();
			

		}

		/// <summary>
		/// Configures the application.
		/// </summary>
		/// <param name="app">The application builder.</param>
		/// <param name="env">The web hosting environment.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseUmbraco()
                .WithMiddleware(u =>
                {
                    u.UseBackOffice();
                    u.UseWebsite();
                })
                .WithEndpoints(u =>
                {
                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    u.UseWebsiteEndpoints();
                });
        }
    }
}
