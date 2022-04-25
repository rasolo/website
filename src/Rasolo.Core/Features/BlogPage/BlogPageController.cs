using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using Anaximapper;
using Microsoft.AspNetCore.Mvc;
using Rasolo.Core.Features.Shared;
using Rasolo.Core.Features.Shared.Compositions;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Rasolo.Core.Features.BlogPage
{
	public class BlogPageController : BaseContentPageController<BlogPage>
	{
		private readonly IPublishedContentMapper anaxiMapper;
		private readonly IBlogPageViewModelFactory _viewModelFactory;

		public BlogPageController(IPublishedContentMapper anaxiMapper, IBlogPageViewModelFactory viewModelFactory) : base(anaxiMapper, viewModelFactory)
		{
			this.anaxiMapper = anaxiMapper;
			this._viewModelFactory = viewModelFactory;
		}

		////TODO: Major refractor
		//public RssResult Feed(ContentModel model)
		//{
			
		//	var blogPage = this._viewModelFactory.CreateModel(null, model);
		//	this.anaxiMapper.Map(model.Content, blogPage);
		//	var blogUrl = "https://" + HttpContext.Request.Host + blogPage.Url + "";

		//	var syndicationItems = new List<SyndicationItem>();

		//	var author = new SyndicationPerson(Rasolo.Services.Constants.Project.Email, "Rasmus Olofsson", "https://rasolo.net");

		//	author.ElementExtensions.Add("title", string.Empty, "Web developer");


		//	author.ElementExtensions.Add("image", string.Empty, "https://rasolo.net/media/5a1pqhxs/rasolo.png");
            
		//	SyndicationFeed feed = new SyndicationFeed(blogPage.Title, blogPage?.MainBody?.ToString().StripHtml(), new System.Uri(Request.Url.AbsoluteUri))
		//	{
		//		Language = "en-us",
		//		Id = blogUrl,
		//		Authors = { author }
		//	};
		//	//var blogPostPages = blogPage.Children;
		//	var blogPostPages = new List<BlogPostPage.BlogPostPage>();
		//	this.anaxiMapper.MapCollection(blogPage.Children, blogPostPages);

		//	foreach (var blogPostPage in blogPostPages)
		//	{
		//		var blogPostPageUrl = "https://" + HttpContext.Request.Host + blogPostPage.Url + "";

		//		var blogPostUri =
		//			new Uri(blogPostPageUrl);
		//		var blogPostContent = new TextSyndicationContent(HttpUtility.HtmlDecode(blogPostPage.MainBody?.ToString().StripHtml()));
		//		var blogPostFeedItem = new SyndicationItem(blogPostPage.Title,
		//			blogPostContent.ToString(),
		//			blogPostUri);
		//		blogPostFeedItem.ElementExtensions.Add("author", string.Empty, Rasolo.Services.Constants.Project.Email);
		//		blogPostFeedItem.PublishDate = blogPostPage.CreateDate;
		//		blogPostFeedItem.LastUpdatedTime = blogPostPage.UpdateDate.ToUniversalTime();


		//		blogPostFeedItem.Content = blogPostContent;

		//		blogPostFeedItem.Categories.Add(new SyndicationCategory(blogPage.Name));
		//		syndicationItems.Add(blogPostFeedItem);
		//	}

		//	feed.LastUpdatedTime = blogPostPages.OrderByDescending(x => x.UpdateDate)
		//		.FirstOrDefault()
		//		.UpdateDate.ToUniversalTime();


		//	feed.Items = syndicationItems;
		//	SyndicationLink link = new SyndicationLink(
		//		new Uri(blogUrl + "feed"), "self", "type", "text/xml", 1000);
		//	feed.Links.Add(link);
			
		//	var result =  new RssResult(feed);

		//	return result;
		//}
	}
}