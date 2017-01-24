# Usage: build compile = compiles application
#        build test = run all tests
#        build docs = show build tasks and dependencies
#        build package = generates nuget package for distribution

param (
    [alias("t")]  [string] $task = "Default",
    [alias("be")] [string] $buildEnvironment = "local",
    [alias("bn")] [string] $buildNumber = "0.1.1.1",
    [alias("bi")] [string] $buildInformation = "Developer Build",
    [alias("p")]  [switch] $projectHelp
)

# This is the psake project file - filled with tasks and dependencies
$projectFileName = ".\build\project.ps1"


if (-not (Get-Module Invoke-PSake)) {
    Import-Module ".\packages\psake.4.4.1\tools\psake.psm1" -Force
}

$psake.use_exit_on_error = $true

$buildNumberParts = $buildNumber.split(".")

if ($buildNumberParts.length -ne 4) {
    Write-Host "Incorrectly formatted Build Number, it must be formatted (n.n.n.n)"
    exit 1
}

$version = "$($buildNumberParts[0]).$($buildNumberParts[1]).$($buildNumberParts[2])";
$buildNumber = $($buildNumberParts[3]);
$informationalVersion = "$version $buildInformation";

Invoke-PSake $projectFileName $task -parameters @{
    version = $version;
    buildNumber = $buildNumber;
    buildEnvironment = $buildEnvironment;
    informationalVersion = $informationalVersion;
}

if ($psake.build_success -eq $false) {
    exit 1
}