using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Xml;
using Anaximapper;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rasolo.Web.Features.Shared;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Rasolo.Web.Features.BlogPage
{
	public class BlogPageController : UmbracoPageController, IVirtualPageController
	{
		private readonly IBlogPageViewModelFactory _blogPageViewModelFactory;
		private readonly IPublishedContentMapper _anaxiMapper;

		public BlogPageController
		(
			IBlogPageViewModelFactory blogPageViewModelFactory,
			ILogger<UmbracoPageController> logger,
			ICompositeViewEngine compositeViewEngine, IPublishedContentMapper anaxiMapper)
			: base(logger, compositeViewEngine)
		{
			_blogPageViewModelFactory = blogPageViewModelFactory;
			_anaxiMapper = anaxiMapper;
		}


		public IPublishedContent FindContent(ActionExecutingContext actionExecutingContext)
		{
			// Resolve services from the container
			var umbracoContextAccessor = actionExecutingContext.HttpContext.RequestServices
				.GetRequiredService<IUmbracoContextAccessor>();
			var publishedValueFallback = actionExecutingContext.HttpContext.RequestServices
				.GetRequiredService<IPublishedValueFallback>();

			var umbracoContext = umbracoContextAccessor.GetRequiredUmbracoContext();
			var path = actionExecutingContext.HttpContext.Request.Path.ToString();
			var x = path.IndexOf("feed");
			var route = path.Substring(0, x);
			var page = umbracoContext.Content.GetByRoute(route);
			var productRoot = umbracoContext.Content.GetById(page.Id);

			return productRoot;
		}

		public IActionResult BlogPage(ContentModel contentModel)
		{
			var viewModel = this._blogPageViewModelFactory.CreateModel(null, contentModel);

			return View(viewModel);
		}

		//TODO: Major refractor
		[Route("/blogs/episerver/feed")]
		[Route("/blogs/it/feed")]
		[Route("/blogs/umbraco/feed")]
		[HttpGet]
		public IActionResult Feed(string id)
		{
			var currentPage = CurrentPage;
			var contentModel = new ContentModel(currentPage);
			var blogPage = this._blogPageViewModelFactory.CreateModel(null, contentModel);
			this._anaxiMapper.Map(contentModel.Content, blogPage);
			var blogUrl = "https://" + HttpContext.Request.Host + blogPage.Url + "";
			
			var syndicationItems = new List<SyndicationItem>();
			
			var author = new SyndicationPerson(Rasolo.Services.Constants.Project.Email, "Rasmus Olofsson", "https://rasolo.net");
			
			author.ElementExtensions.Add("title", string.Empty, "Web developer");
			
			
			author.ElementExtensions.Add("image", string.Empty, "https://rasolo.net/media/5a1pqhxs/rasolo.png");
			         
			SyndicationFeed feed = new SyndicationFeed(blogPage.Title, blogPage?.MainBody?.ToString().StripHtml(), new System.Uri(Request.GetDisplayUrl()))
			{
				Language = "en-us",
				Id = blogUrl,
				Authors = { author }
			};
			//var blogPostPages = blogPage.Children;
			var blogPostPages = new List<BlogPostPage.BlogPostPage>();
			this._anaxiMapper.MapCollection(blogPage.Children, blogPostPages);
			
			foreach (var blogPostPage in blogPostPages)
			{
				var blogPostPageUrl = "https://" + HttpContext.Request.Host + blogPostPage.Url + "";
			
				var blogPostUri =
					new Uri(blogPostPageUrl);
				var blogPostContent = new TextSyndicationContent(HttpUtility.HtmlDecode(blogPostPage.MainBody?.ToString().StripHtml()));
				var blogPostFeedItem = new SyndicationItem(blogPostPage.Title,
					blogPostContent.ToString(),
					blogPostUri);
				blogPostFeedItem.ElementExtensions.Add("author", string.Empty, Rasolo.Services.Constants.Project.Email);
				blogPostFeedItem.PublishDate = blogPostPage.CreateDate;
				blogPostFeedItem.LastUpdatedTime = blogPostPage.UpdateDate.ToUniversalTime();
			
			
				blogPostFeedItem.Content = blogPostContent;
			
				blogPostFeedItem.Categories.Add(new SyndicationCategory(blogPage.Name));
				syndicationItems.Add(blogPostFeedItem);
			}
			
			feed.LastUpdatedTime = blogPostPages.OrderByDescending(x => x.UpdateDate)
				.FirstOrDefault()!
				.UpdateDate.ToUniversalTime();
			
			
			feed.Items = syndicationItems;
			SyndicationLink link = new SyndicationLink(
				new Uri(blogUrl + "feed"), "self", "type", "text/xml", 1000);
			feed.Links.Add(link);

			using (var stream = new MemoryStream())
			{
				using (var xmlWriter = XmlWriter.Create(stream, GetSettings()))
				{
					var rssFormatter = new Rss20FeedFormatter(feed, false);
					rssFormatter.WriteTo(xmlWriter);
					xmlWriter.Flush();
				}

				return File(stream.ToArray(), "text/xml; charset=utf-8");
			}


		}

		private XmlWriterSettings GetSettings()
		{
			return new XmlWriterSettings
			{
				Encoding = Encoding.UTF8,
				NewLineHandling = NewLineHandling.Entitize,
				NewLineOnAttributes = true,
				Indent = true
			};
		}
	}
}