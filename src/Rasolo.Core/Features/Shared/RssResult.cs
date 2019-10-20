using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Rasolo.Core.Features.Shared
{
	public class RssResult : FileResult
	{
		private readonly SyndicationFeed _feed;

		public RssResult(SyndicationFeed feed)
			: base("application/rss+xml")
		{
			_feed = feed;
		}

		protected override void WriteFile(HttpResponseBase response)
		{
			using (var writer = XmlWriter.Create(response.OutputStream))
			{
				_feed.GetAtom10Formatter().WriteTo(writer);
			}
		}
	}
}