function AddBinding($ipBinding, $addressBinding){
	$File = "C:\Windows\System32\drivers\etc\hosts"
	$FileContent = Get-Content $File
	if (! ($FileContent -contains "$ipBinding $addressBinding" )){
		Add-Content $File "$ipBinding $addressBinding"
		Write-Host "Added address binding $ipBinding $addressBinding to $File" -foregroundColor green
	} else {
		Write-Host "The address binding already exist in the hosts file! Check $File to make sure it is correct." -foregroundcolor Yellow
	}
}