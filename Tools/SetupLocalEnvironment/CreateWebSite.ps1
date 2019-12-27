Import-Module WebAdministration
$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

$siteName      = 'rasolo.local'
$appPoolName   = 'rasolo.local'
$aspNetVersion = 'v4.0'
$port          = '80'
$path          = (((get-item $scriptDir).parent.parent.FullName) + "\src\Rasolo.Web")
$user          = '' # Optional (ApplicationPoolIdentity will be set if user is not provided)
$password      = '' # Optional
$anonymousAuthentication	= $true
$windowsAuthentication		= $false
$basicAuthentication		= $false
$formsAuthentication		= $false

. "$scriptDir\CreateWebSite\CreateWebSite.ps1" $siteName $appPoolName $aspNetVersion $port $path $user $password

# Set bindings in C:\Windows\System32\drivers\etc\hosts
. "$scriptDir\CreateWebSite\Set-Hosts.ps1"
AddBinding "#rasolo.local" "" 
AddBinding "127.0.0.1" "rasolo.local"

# Set Auth settings in IIS
. "$scriptDir\CreateWebSite\SetAuthSettings.ps1" $siteName $anonymousAuthentication $windowsAuthentication $basicAuthentication $formsAuthentication
