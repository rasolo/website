﻿
namespace Rasolo.Web.Features.Shared.GlobalSettings
{
	public interface IGlobalSettingsPageViewModel : IGlobalSettingsPage
	{
		bool ShowCookiesNotice { get; }
	}
}
