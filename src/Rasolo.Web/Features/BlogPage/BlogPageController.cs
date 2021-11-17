using Anaximapper;
using Rasolo.Web.Features.Shared.Compositions;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Rasolo.Web.Features.BlogPage
{
	public class BlogPageController : BaseContentPageController<BlogPage>
	{
		private readonly IPublishedContentMapper anaxiMapper;
		private readonly IBlogPageViewModelFactory _viewModelFactory;

		public BlogPageController(IPublishedContentMapper anaxiMapper, IBlogPageViewModelFactory viewModelFactory, ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(anaxiMapper, viewModelFactory, logger, compositeViewEngine, umbracoContextAccessor)
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