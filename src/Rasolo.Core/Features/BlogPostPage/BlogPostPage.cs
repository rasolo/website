using System;
using AutoMapper.Configuration.Annotations;
using Rasolo.Core.Features.Shared.UI;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPage : BaseContentPage
	{
		public virtual string Preamble { get; set; }
		[Ignore]
		public DateTime CreatedDate { get; set; }
		[Ignore]
		public string PageUrl { get; set; }
	}
}