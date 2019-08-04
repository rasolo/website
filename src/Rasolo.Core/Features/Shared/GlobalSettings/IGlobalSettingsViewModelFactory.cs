namespace Rasolo.Core.Features.Shared.GlobalSettings
{
	public interface IGlobalSettingsViewModelFactory
	{
		GlobalSettingsViewModel CreateModel(GlobalSettingsViewModel viewModel);
	}
}
