[CmdletBinding()]
param(            
    [Parameter(Mandatory=$true)]
    [string]$siteName,
    [Parameter(Mandatory=$true)]
    [string]$appPoolName,
    [Parameter(Mandatory=$true)]
    [string]$aspNetVersion,
    [Parameter(Mandatory=$true)]
    [string]$port,
    [Parameter(Mandatory=$true)]
    [string]$path,
    [Parameter(Mandatory=$false)]
    [string]$user,
    [Parameter(Mandatory=$false)]
    [string]$password
)
If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{
[console]::ForegroundColor = "Red"   
$name = Read-host "Powershell is not running with administrative rights! The createWebSite script will fail!"
[console]::ResetColor()
break
}    

$backupName = "$(Get-date -format "yyyyMMdd-HHmmss")-$siteName"
Write-Host "Backing up IIS config to backup named $backupName"
$backup = Backup-WebConfiguration $backupName
 
try { 
    # Delete the website & app pool if needed
    if (Test-Path "IIS:\Sites\$siteName") {
        Write-host "Removing existing website $siteName"
        Remove-Website -Name $siteName
    }
 
    if (Test-Path "IIS:\AppPools\$appPoolName") {
        Write-host "Removing existing AppPool $appPoolName"
        Remove-WebAppPool -Name $appPoolName
    }
 
    # Promt remove anything already using that port
    foreach($site in Get-ChildItem IIS:\Sites) {
        if( $site.Bindings.Collection.bindingInformation -eq ("*:" + $port + ":")){
			[console]::ForegroundColor = "Yellow"  
            $answer = Read-Host "Warning: Found an existing site '$($site.Name)' already using port $port. Removing it? (yes/no)"
			[console]::ResetColor()
            if ($answer -eq 'yes'){
                Remove-Website -Name  $site.Name 
                Write-Host "Website $($site.Name) was removed" -foregroundcolor red
            } else {
                Write-Host "Website $($site.Name) was not removed"
            }
        }
    }
 
    Write-host "Create an appPool named $appPoolName under $aspNetVersion runtime, Integrated mode"
    $pool = New-WebAppPool $appPoolName
    $pool.managedRuntimeVersion = $aspNetVersion
    $pool.processModel.identityType = 4 # ApplicationPoolIdentity
	
	if ($user -ne '' -AND $password -ne '') {
	    Write-Host "Setting AppPool to run as $user"
		$pool.processmodel.identityType = 3
		$pool.processmodel.username = $user
		$pool.processmodel.password = $password
	} 
	
    $pool | Set-Item
 
    if ((Get-WebAppPoolState -Name $appPoolName).Value -ne "Started") {
        throw "WARNING! App pool $appPoolName was created but did not start automatically."
    }
 
    Write-Host "Create a website $siteName from directory $path on port $port"
    $website = New-Website -Name $siteName -PhysicalPath $path -ApplicationPool $appPoolName -Port $port -HostHeader $siteName
 
    if ((Get-WebsiteState -Name $siteName).Value -ne "Started") {
        throw "WARNING! Website $siteName was created but did not start automatically."
    }

    Write-Host "Website and AppPool created and started successfully" -foregroundcolor green
} catch {
    Write-Host "WARNING! Error detected, running command 'Restore-WebConfiguration $backupName' to restore the web server to its initial state. Please wait..." -foregroundcolor red
    # Allow backup to unlock files
    sleep 3
    Restore-WebConfiguration $backupName
    Write-Host "IIS Restore complete." 
    throw
}