using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using Rasolo.Core.Features.Shared;
using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Core;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPageController : BaseContentPageController<BlogPage>
	{
		private readonly IUmbracoMapper _umbracoMapper;
		private readonly IBlogPageViewModelFactory _viewModelFactory;

		public BlogPageController(IUmbracoMapper umbracoMapper, IBlogPageViewModelFactory viewModelFactory) : base(umbracoMapper, viewModelFactory)
		{
			this._umbracoMapper = umbracoMapper;
			this._viewModelFactory = viewModelFactory;
		}

		public new ActionResult Index(ContentModel model)
		{
			var blogPage = this._viewModelFactory.CreateModel(model);


			return View(blogPage);
		}

	

		//TODO: Major refractor
		public ActionResult Feed(ContentModel model)
		{
			var blogPage = this._viewModelFactory.CreateModel(model);
			this._umbracoMapper.Map(model.Content, blogPage);
			var blogUrl = "http://" + Request.Url.Host  + blogPage.Url + "";

			var syndicationItems = new List<SyndicationItem>();

			var author = new SyndicationPerson("rasmusolofssons@gmail.com", "Rasmus Olofsson", "http://rasolo.azurewebsites.net");

			author.ElementExtensions.Add("title", string.Empty, "Web developer");


			author.ElementExtensions.Add("image", string.Empty, "http://rasolo.azurewebsites.net/media/5a1pqhxs/rasolo.png");
            
			SyndicationFeed feed = new SyndicationFeed(blogPage.Title, blogPage?.MainBody?.ToString().StripHtml(), new System.Uri(Request.Url.AbsoluteUri))
			{
				Language = "en-us",
				Id = blogUrl,
				Authors = { author }
			};
			//var blogPostPages = blogPage.Children;
			var blogPostPages = new List<BlogPostPage.BlogPostPage>();
			this._umbracoMapper.MapCollection(blogPage.Children, blogPostPages);

			foreach (var blogPostPage in blogPostPages)
			{
				var blogPostPageUrl = "http://" + Request.Url.Host + blogPostPage.Url + "";

				var blogPostUri =
					new Uri(blogPostPageUrl);
				var blogPostContent = new TextSyndicationContent(HttpUtility.HtmlDecode(blogPostPage.MainBody?.ToString().StripHtml()));
				var blogPostFeedItem = new SyndicationItem(blogPostPage.Title,
					blogPostContent.ToString(),
					blogPostUri);
				blogPostFeedItem.ElementExtensions.Add("author", string.Empty, "rasmusolofssons@gmail.com");
				blogPostFeedItem.PublishDate = blogPostPage.CreateDate;
				blogPostFeedItem.LastUpdatedTime = blogPostPage.UpdateDate.ToUniversalTime();


				blogPostFeedItem.Content = blogPostContent;

				blogPostFeedItem.Categories.Add(new SyndicationCategory(blogPage.Name));
				syndicationItems.Add(blogPostFeedItem);
			}

			feed.LastUpdatedTime = blogPostPages.OrderByDescending(x => x.UpdateDate)
				.FirstOrDefault()
				.UpdateDate.ToUniversalTime();


			feed.Items = syndicationItems;
			SyndicationLink link = new SyndicationLink(
				new Uri(blogUrl + "feed"), "self", "type", "application/rss+xml", 1000);
			feed.Links.Add(link);
			return new RssResult(feed);
		}
	}
}