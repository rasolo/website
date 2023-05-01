using Examine.Lucene;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Util;
using Microsoft.Extensions.Options;
using Constants = Umbraco.Cms.Core.Constants;

namespace Rasolo.Web.Features.SearchPage.Examine
{
	public class ConfigureExternalIndexOptions : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
	{
		public void Configure(string name, LuceneDirectoryIndexOptions options)
		{
			if (name.Equals(Constants.UmbracoIndexes.ExternalIndexName))
			{
				options.Analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48, new CharArraySet(LuceneVersion.LUCENE_48, System.Array.Empty<string>(), true));
			}
		}

		// Part of the interface, but does not need to be implemented for this.
		public void Configure(LuceneDirectoryIndexOptions options)
		{
			throw new System.NotImplementedException();
		}
	}
}
