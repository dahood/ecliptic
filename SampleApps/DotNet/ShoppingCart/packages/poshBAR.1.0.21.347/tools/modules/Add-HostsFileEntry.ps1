<#
    .DESCRIPTION
        Adds the specified line to the hosts file if it doesn't already exist.

    .EXAMPLE
        Add-HostsFileEntry "local.example.ca"

    .EXAMPLE
        Add-HostsFileEntry "local.example.ca" "10.1.1.1"

    .EXAMPLE
        Add-HostsFileEntry "local.example.ca" -includeLoopbackFix

    .PARAMETER ipAddress
        The ip address of the machine you want to target.

    .PARAMETER hostName
        The "host name" name you want routed to the target ip address.

    .PARAMETER includeLoopbackFix
        Switch to determine if the Loopback Fix should also be applied (Add-LoopbackFix)

    .SYNOPSIS
        Will add a hosts file entry for the host name specified targeting the specified ip address.

    .NOTES
        If you '-includeLoopbackFix', this method will call the 'Add-LoopbackFix' module (see Links below) 

    .LINK
        Add-LoopbackFix
#>
function Add-HostsFileEntry
{
    [CmdletBinding()]
	param(
        [parameter(Mandatory=$true,position=0)] [string] [alias('hn')]$hostName,
        [parameter(Mandatory=$false,position=1)] [ValidatePattern('\d{1,3}(\.\d{1,3}){3}')] [string] [alias('ip')] $ipAddress = "127.0.0.1",
        [parameter(Mandatory=$false,position=2)][switch] $includeLoopbackFix
	)
    $ErrorActionPreference = "Stop"
    Write-Host ($msgs.msg_add_host_entry -f $hostName) -NoNewLine

	$HostsLocation = "$env:windir\System32\drivers\etc\hosts"
	$NewHostEntry = "`t$ipAddress`t$hostName"

	if((gc $HostsLocation) -contains $NewHostEntry)
	{
        Write-Host "`tExists" -f Cyan
	}
	else
	{
        if($poshBAR.DisableHostFileAdministration){
            Write-Host # just a line break
            Write-Warning $msgs.wrn_host_file_admin_disabled
        } else {
    		Add-Content -Path $HostsLocation -Value $NewHostEntry
            Write-Host "`tDone"
        }
	}

	# Validate entry
    Write-Host ($msgs.msg_validate_host_entry -f $hostName) -NoNewLine
	if((gc $HostsLocation) -contains $NewHostEntry)
	{
        Write-Host "`tPassed" -f Green
	}
	else
	{
        Write-Host "`tFailed" -f Red
	}

    if($includeLoopbackFix.IsPresent){
        Add-LoopbackFix $hostName
    }
}