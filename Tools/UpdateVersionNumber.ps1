[CmdletBinding()]
Param(
	[Parameter(Mandatory=$True)]
	[ValidateNotNullOrEmpty()]
	[System.String]
	$Major = ""
	,
	[Parameter(Mandatory=$True)]
	[ValidateNotNullOrEmpty()]
	[System.String]
	$Minor = ""
	,
	[Parameter(Mandatory=$True)]
	[ValidateNotNullOrEmpty()]
	[System.String]
	$Build = ""
)
$ScriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
$Path = (Join-Path -Path $ScriptDir -ChildPath ".." -Resolve)
function Update-SourceVersion {
	$AssemblyVersion = 'AssemblyVersion("' + $Major + "." + $Minor + "." + $Build + '.0")';
	$AssemblyFileVersion = 'AssemblyFileVersion("' + $Major + "." + $Minor + "." + $Build + '.0")';
	$AssemblyInformationalVersion = 'AssemblyInformationalVersion("' + $Major + "." + $Minor + "." + $Build + '")';
	
	Write-Verbose $AssemblyVersion
	Write-Verbose $AssemblyFileVersion
	Write-Verbose $AssemblyInformationalVersion
 
	foreach ($o in $input) {
		Write-Verbose "Updating '$($o.FullName)'"

		$AssemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
		$AssemblyFileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
		$AssemblyInformationalVersionPattern = 'AssemblyInformationalVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
		(Get-Content $o.FullName) | ForEach-Object {
			% {$_ -replace $AssemblyVersionPattern, $AssemblyVersion } |
			% {$_ -replace $AssemblyFileVersionPattern, $AssemblyFileVersion } |
			% {$_ -replace $AssemblyInformationalVersionPattern, $AssemblyInformationalVersion }
		} | Out-File $o.FullName -encoding UTF8 -force
	}
}

function Update-AllAssemblyInfoFiles {
	Write-Verbose "Searching '$Path'"
	foreach ($File in "AssemblyInfo.cs") {
		Get-ChildItem $Path -Recurse |? {$_.Name -eq $File} | Update-SourceVersion; 
	}
}
 
Update-AllAssemblyInfoFiles