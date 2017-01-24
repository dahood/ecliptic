<#
    .DESCRIPTION
        Tests if this powershell invocation is being run as administrator.

    .EXAMPLE
        Test-RunAsAdministrator

    .SYNOPSIS
        Tests if this powershell invocation is being run as administrator, will break and display an error if it is not.

    .NOTES
        Nothing yet...
#>
function Test-RunAsAdmin
{

    $ErrorActionPreference = "Stop"

    If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
    {
        Write-Error $msgs.error_admin_required
    }
}
