namespace Rasolo.Core.Features.Shared.UI
{
	public abstract class BaseContentPage : IContentPage
	{
		public virtual string Name { get; set; }
		public virtual string Title { get; set; }
	}
}