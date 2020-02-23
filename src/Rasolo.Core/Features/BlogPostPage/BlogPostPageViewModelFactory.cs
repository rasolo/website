using Rasolo.Core.Features.Shared.Abstractions.UmbracoHelper;
using Rasolo.Core.Features.Shared.Compositions;
using Rasolo.Infrastructure.Repositories;
using Umbraco.Web.Models;
using Zone.UmbracoMapper.V8;

namespace Rasolo.Core.Features.BlogPostPage
{
	public class BlogPostPageViewModelFactory : BaseContentPageViewModelFactory<BlogPostPage>, IBlogPostPageViewModelFactory
	{
		private readonly ICommentsRepository _commentsRepository;

		public BlogPostPageViewModelFactory(IUmbracoMapper umbracoMapper, IUmbracoHelper umbracoHelper,
			ICommentsRepository commentsRepository) : base(umbracoMapper, umbracoHelper)
		{
			_commentsRepository = commentsRepository;
		}
		public BlogPostPage CreateModel(BlogPostPage viewModel, ContentModel contentModel)
		{
			SetViewModelProperties(viewModel, contentModel);
			return viewModel;
		}

		public override void SetViewModelProperties(BlogPostPage viewModel, ContentModel contentModel)
		{
			base.SetViewModelProperties(viewModel, contentModel);
			viewModel.Comments = this._commentsRepository.GetByContentId(contentModel.Content.Id);
		}
	}
}