
namespace Rasolo.Web.Features.Shared.GlobalSettings
{
	public interface IGlobalSettingsPageViewModelFactory
	{
		GlobalSettingsPageViewModel CreateModel(string id);
	}
}
