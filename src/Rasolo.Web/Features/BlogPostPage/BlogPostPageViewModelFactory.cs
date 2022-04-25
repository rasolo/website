using Anaximapper;
using Rasolo.Web.Features.Shared.Compositions;
using Rasolo.Services.Abstractions.UmbracoHelper;
using Umbraco.Cms.Core.Models;

namespace Rasolo.Web.Features.BlogPostPage
{
	public class BlogPostPageViewModelFactory : BaseContentPageViewModelFactory<BlogPostPage>, IBlogPostPageViewModelFactory
	{
		//private readonly ICommentsRepository _commentsRepository;

		public BlogPostPageViewModelFactory(IPublishedContentMapper anaxiMapper, IUmbracoHelper umbracoHelper
			/*ICommentsRepository commentsRepository*/) : base(anaxiMapper, umbracoHelper)
		{
			//_commentsRepository = commentsRepository;
		}

		public override BlogPostPage CreateModel(BlogPostPage viewModel, ContentModel contentModel)
		{
			viewModel = base.CreateModel(viewModel, contentModel);

			SetViewModelProperties(viewModel, contentModel);
			return viewModel;
		}

		public override void SetViewModelProperties(BlogPostPage viewModel, ContentModel contentModel)
		{
			base.SetViewModelProperties(viewModel, contentModel);
			if (contentModel != null)
			{
				//viewModel.Comments = this._commentsRepository.GetByContentId(contentModel.Content.Id);
			}
			viewModel.ShowTeaserMediaAltText = !string.IsNullOrEmpty(viewModel.TeaserMediaAltText);
			viewModel.TeaserHeading = viewModel.TeaserHeading = !string.IsNullOrEmpty(viewModel.TeaserHeading) ? viewModel.TeaserHeading : !string.IsNullOrEmpty(viewModel.Title) ? viewModel.Title : viewModel.Name;
		}
	}
}