using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Xml;
using Anaximapper;
using Rasolo.Services.Constants;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Rasolo.Web.Features.Feed
{
	public class FeedPageViewModelFactory : IFeedPageViewModelFactory<FeedPage>
	{
		private readonly IPublishedContentMapper _anaxiMapper;

		public FeedPageViewModelFactory(IPublishedContentMapper anaxiMapper)
		{
			_anaxiMapper = anaxiMapper;
		}

		public MemoryStream CreateModel(FeedPage viewModel, ContentModel contentModel)
		{
			return Feed(viewModel, contentModel.Content);
		}

		public MemoryStream Feed(FeedPage viewModel, IPublishedContent content)
		{
			var blogPagePublishedContent = content.Parent;
			var blogPage = new BlogPage.BlogPage();
			_anaxiMapper.Map(blogPagePublishedContent, blogPage);

			var syndicationItems = new List<SyndicationItem>();

			var author = new SyndicationPerson(Project.Email, "Rasmus Olofsson",
				"https://rasolo.net");

			author.ElementExtensions.Add("title", string.Empty, "Web developer");


			author.ElementExtensions.Add("image", string.Empty, "https://rasolo.net/media/5a1pqhxs/rasolo.png");

			var feed = new SyndicationFeed(blogPage.Title, blogPage?.MainBody?.ToString().StripHtml(),
				new Uri(viewModel.AbsoluteUrl))
			{
				Language = "en-us",
				Id = blogPage.AbsoluteUrl,
				Authors = { author }
			};

			var blogPostPages = new List<BlogPostPage.BlogPostPage>();
			_anaxiMapper.MapCollection(blogPage.Children, blogPostPages);
			blogPostPages.Remove(blogPostPages.FirstOrDefault(x => x.Name == "Feed"));

			foreach (var blogPostPage in blogPostPages)
			{
				var blogPostContent =
					new TextSyndicationContent(HttpUtility.HtmlDecode(blogPostPage.MainBody?.ToString().StripHtml()));
				var blogPostFeedItem = new SyndicationItem(blogPostPage.Title,
					blogPostContent.ToString(),
					new Uri(blogPostPage.AbsoluteUrl));
				blogPostFeedItem.ElementExtensions.Add("author", string.Empty, Project.Email);
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
			var link = new SyndicationLink(
				new Uri(viewModel.AbsoluteUrl), "self", "type", "text/xml", 1000);
			feed.Links.Add(link);

			using (var stream = new MemoryStream())
			{
				using (var xmlWriter = XmlWriter.Create(stream, GetSettings()))
				{
					var rssFormatter = new Rss20FeedFormatter(feed, false);
					rssFormatter.WriteTo(xmlWriter);
					xmlWriter.Flush();
				}

				return stream;
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