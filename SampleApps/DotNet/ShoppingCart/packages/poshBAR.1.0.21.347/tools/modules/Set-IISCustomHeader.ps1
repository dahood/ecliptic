$appcmd = "$env:windir\system32\inetsrv\appcmd.exe"
<#
    .DESCRIPTION
       Will set the specified Authentication value for the specified applicaiton or website

    .EXAMPLE
        Set-IISCustomHeader "xyz.mysite.com" "access-control-allow-origin" "*"

    .PARAMETER siteName
        The name of the site to add custom header to.

    .PARAMETER customHeaderName
        The name of the custom header to add.

    .PARAMETER customHeaderValue
        The value of the custom header to add.

    .SYNOPSIS
        Will set a custom header to specified value on the site indicated.
#>

function Set-IISCustomHeader
{
    [CmdletBinding()]
    param(
        [parameter(Mandatory=$true,position=0)] [string] $siteName,
        [parameter(Mandatory=$true,position=1)] [string] $customHeaderName,
        [parameter(Mandatory=$true,position=2)] [string] $customHeaderValue
    )

    $ErrorActionPreference = "Stop"

    Write-Host ($msgs.msg_custom_header -f $customHeaderName, $siteName, $customHeaderValue) -NoNewLine
    
    Exec { Invoke-Expression "$appcmd set config $siteName -section:system.webServer/httpProtocol /+`"customHeaders.[name='$customHeaderName',value='$customHeaderValue']`""} -retry 10  | Out-Null

    Write-Output "`tDone" -f Green
}

