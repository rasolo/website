namespace Rasolo.Core.Features.Shared.GlobalSettings
{
	public class GlobalSettingsViewModelFactory : IGlobalSettingsViewModelFactory
	{
		public GlobalSettingsViewModel CreateModel(GlobalSettingsViewModel viewModel)
		{
			viewModel.HomeText = !string.IsNullOrEmpty(viewModel.HomeText) ? viewModel.HomeText : string.Empty;

			return viewModel;
		}
	}
}