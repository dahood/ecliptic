<#
    .SYNOPSIS
        Invokes the deployment steps on the target machine

    .EXAMPLE
        Invoke-Deployment

    .DESCRIPTION
        Call this after all deployment steps are defined within your deploy script. 
        This method will iterate over all of the deployment steps, execute the script block, and capture the timing.

#>
function Invoke-Deployment {
    [CmdletBinding()]
    param()
    $currentContext = $poshBARDeploy.context.Peek()
    $deploymentStopWatch = [System.Diagnostics.Stopwatch]::StartNew()
    $stepList = $currentContext.steps
    if ($stepList) {
        $stepList.Keys | %{
            Invoke-Step $_
        }
    }
    WriteStepTimeSummary $deploymentStopWatch.Elapsed
    $poshBARDeploy = $null
}

function Invoke-Step {
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)] [string]$stepName
    )

    $stepKey = $stepName.ToLower()
    $currentContext = $poshBARDeploy.context.Peek()
    $step = $currentContext.steps.$stepKey
    
    Format-TaskNameToHost $step.Name

    $stopwatch = [System.Diagnostics.Stopwatch]::StartNew()
    & $step.Action
    $step.Duration = $stopwatch.Elapsed
}

function Initialize-Context{
    $script:poshBARDeploy = @{}
    $poshBARDeploy.context = new-object system.collections.stack
    $poshBARDeploy.context.push(@{
        "steps" = New-Object System.Collections.Specialized.OrderedDictionary}
    )
}

function WriteStepTimeSummary($totalDeploymentDuration) {
    if ($poshBARDeploy.context.count -gt 0) {
        Write-Host "Deployment Complete" -f Green
        "-" * 70
        "Deployment Report"
        "-" * 70
        $list = @()
        $currentContext = $poshBARDeploy.context.Peek()

        $stepList = $currentContext.steps
        if ($stepList) {
            $stepList.Keys | %{
                $stepForReport = $currentContext.steps.$_
                $list += new-object PSObject -property @{
                    Name = $stepForReport.Name;
                    Duration = $stepForReport.Duration
                }
            }
        }

        $list += new-object PSObject -property @{
            Name = "Total:";
            Duration = $totalDeploymentDuration
        }
        # using "out-string | where-object" to filter out the blank line that format-table prepends
        $list | format-table -autoSize -property Name,Duration | out-string -stream | where-object { $_ }
    }
}


<#
    .SYNOPSIS
        Creates a deployment step that verifies that the required windows features are installed

    .DESCRIPTION
        When certain Windows Features are required for an application to run, simply add those required windows features to an array and pass them into this function

    .PARAMETER requiredWindowsFeatures
        The collection of windows features required.

    .EXAMPLE
        RequiredWindowsFeatures @('IIS-WebServer','IIS-WebServerRole')
#>
function RequiredWindowsFeatures {
    [CmdletBinding()]
    param([string[]] $requiredWindowsFeatures)

    $script:requiredWindowsFeatures = $requiredWindowsFeatures
    Step InstallRequiredWindowsFeatures {
        Install-WindowsFeatures $script:requiredWindowsFeatures
    }
}
set-alias RequiredFeatures RequiredWindowsFeatures

function Step{
    [CmdletBinding()]
    param(
        [parameter(Mandatory=$true,position=0)] [string] $name,
        [parameter(Mandatory=$true,position=1)] [scriptblock] $action
    )
    $newStep = @{
        Name = $name
        Action = $action
    }

    $stepKey = $name.ToLower()
    
    if(!$poshBARDeploy){
        Initialize-Context
    }

    $currentContext = $poshBARDeploy.context.Peek()
    #Assert (!$currentContext.steps.ContainsKey($stepKey)) ($msgs.error_duplicate_step_name -f $name)
        
    $currentContext.steps.$stepKey = $newStep
}