[CmdletBinding()]
param(            
    [Parameter(Mandatory=$true)]
    [string]$siteName,
	[Parameter(Mandatory=$true)]
    [bool]$anonymousAuthentication,
	[Parameter(Mandatory=$true)]
    [bool]$windowsAuthentication,
	[Parameter(Mandatory=$true)]
    [bool]$basicAuthentication,
	[Parameter(Mandatory=$true)]
    [bool]$formsAuthentication
)

Import-Module WebAdministration

Write-host "Configure authentication for IIS..." 

Set-WebConfigurationProperty `
-filter "/system.webServer/security/authentication/windowsAuthentication" `
-name enabled -value $windowsAuthentication -PSPath "IIS:\" -location $siteName
Write-host "Windows authentication set to $windowsAuthentication on $siteName" -foregroundcolor green

Set-WebConfigurationProperty `
-filter "/system.webServer/security/authentication/anonymousAuthentication" `
-name enabled -value $anonymousAuthentication -PSPath "IIS:\" -location $siteName
Write-host "Anonymous authentication set to $anonymousAuthentication on $siteName" -foregroundcolor green

Set-WebConfigurationProperty `
-filter "/system.webServer/security/authentication/basicAuthentication" `
-name enabled -value $basicAuthentication -PSPath "IIS:\" -location $siteName
Write-host "Basic authentication set to $basicAuthentication on $siteName" -foregroundcolor green

$config = (Get-WebConfiguration system.web/authentication "IIS:\" -location $siteName)
$config.mode = "Forms"
$config | Set-WebConfiguration system.web/authentication
Write-host "Forms authentication set to $formsAuthentication on $siteName" -foregroundcolor green
