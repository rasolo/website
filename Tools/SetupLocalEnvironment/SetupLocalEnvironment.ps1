[CmdletBinding()]
$SiteName = "rasolo.local"
$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

while(-Not ($step -eq 0) -Or ($NULL -eq $step)){
Write-Host "`nRasolo"
Write-Host "Local Environment Setup`n"	
Write-Host "Available operations (run as Administrator):`n"
	Write-Host "1. Install IIS website, app pools and hosts entries`n2. Recycle app pool`n3. Exit`n"

	$step = Read-Host "Choose operation"

	if($step -eq 2 -Or $step -eq 1){
        Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass

		& $scriptDir\CreateWebSite.ps1
		Write-Host "Installed IIS website, app pools and hosts entries`n" -foregroundColor green
	}
	if($step -eq 3){
		Write-Host "Goodbye.`n" -foregroundcolor green
		$step = 0
	}
}