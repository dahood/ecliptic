properties { 

    # These params are passed in via the calling script
    Write-Host " "
    Write-Host "Version: $version"
    Write-Host "BuildNumber: $buildNumber"
    Write-Host "BuildEnvironment: $buildEnvironment"
    Write-Host "InformationalVersion: $informationalVersion"
    Write-Host " "
	
 	$buildScriptDir  = resolve-path .
    $buildProjectFile = "$buildScriptDir\project.ps1"
	$baseDir  = resolve-path "$buildScriptDir\.."
    $modulesDir = "$buildScriptDir\modules"
    $buildDir = "$baseDir\build-artifacts"     
    $buildOutputDir = "$buildDir\output" 
    $buildTestResultsDir = "$buildDir\results" 
    $buildPublishDir = "$buildDir\publish"
    $specflowToolsDir = "$packagesDir\SpecFlow.1.9.0\tools"
	$nugetSpecFile = "$buildScriptDir\ecliptic.nuspec"
	
    $solutionFile = "$baseDir\Ecliptic.sln"
	$buildConfig = "Debug" 
    $unitTestAssemblyFile = "$buildOutputDir\Ecliptic.Unit.Test.dll"
    $unitTestNamespace = "Ecliptic.Unit.Test";
	$unitTestProjectName = "$baseDir\src\Unit.Test\Unit.Test.csproj"
	$unitTestAppConfig = "$baseDir\src\Unit.Test\App.config"
	
    $packagesDir = "$baseDir\packages"
	$toolsDir = "$baseDir\tools"  
    $buildFilesDir = "$baseDir\build"
  	$nunitRunnerDir = "$packagesDir\NUnit.Runners.2.6.4\tools"
	$nugetRunnerDir = "$packagesDir\NuGet.CommandLine.2.8.5\tools"
}

task default -depends Test

Task Compile -depends Init, SetVersion { 
    Write-Host "msbuild /t:build /p:OutDir="$buildOutputDir\" $solutionFile /p:Configuration="$buildConfig" /p:VisualStudioVersion=12.0 /maxcpucount"
	exec { msbuild /t:build /p:OutDir="$buildOutputDir\" $solutionFile /p:Configuration="$buildConfig" /p:VisualStudioVersion=12.0 /maxcpucount } "Error compiling the solution."
	cp $unitTestProjectName $buildOutputDir
	cp $unitTestAppConfig $buildOutputDir
} 

task Test -depends UnitTest

task UnitTest -depends Compile {
    Invoke-Nunit $unitTestAssemblyFile $buildTestResultsDir $unitTestNamespace
}

task Docs {
    Invoke-PSake $buildProjectFile -docs
}

task Package -depends SetReleaseInfo, Compile {
	Invoke-Nuget
}

task SetReleaseInfo {
    $buildConfig = "Release"
}

task PostBuildEvent {
  Copy-Item $\*.* $postBuildOutoutDir
}

Task Init -depends SetupPaths, ImportModules, Clean, MakeBuildDir

Task SetupPaths {
    # Adding some of our tools to the Path so we can run them easier
    $env:Path += ";$nunitRunnerDir"
	$env:Path += ";$nugetRunnerDir"
    $env:Path += ";$specflowToolsDir"
    $env:Path += ";$eclipticDir"
}

Task ImportModules {
    Import-Module "$modulesDir\Update-AssemblyVersions"
}

task Clean { 
  remove-item -force -recurse $buildOutputDir -ErrorAction SilentlyContinue 
  remove-item -force -recurse $buildPublishDir -ErrorAction SilentlyContinue 
  remove-item -force -recurse $buildTestResultsDir -ErrorAction SilentlyContinue 
  New-Item -ItemType Directory -Force -Path $buildOutputDir
  New-Item -ItemType Directory -Force -Path $buildPublishDir 
  New-Item -ItemType Directory -Force -Path $buildTestResultsDir
}

Task SetVersion -depends Init {
    Update-AssemblyVersions $version $buildNumber $informationalVersion
}

Task MakeBuildDir {
    New-Item -ItemType Directory -Force -Path $buildOutputDir
    New-Item -ItemType Directory -Force -Path $buildPublishDir
    New-Item -ItemType Directory -Force -Path $buildTestResultsDir
}


######## PowerShell Functions - To be moved to /modules directory eventually ###############################
Function Invoke-Nunit ( [string] $targetAssembly, [string] $outputDir, [string] $runCommand ) {
    $fileName = Get-TestFileName $outputDir $runCommand

    $xmlFile = "$fileName.xml"
    $txtFile = "$fileName.txt"

    Write-Host "nunit-console.exe $targetAssembly /fixture:$runCommand /xml=$xmlFile /out=$txtFile /nologo /framework=4.0"
    
    exec { nunit-console.exe $targetAssembly /fixture:$runCommand /xml=$xmlFile /out=$txtFile /nologo /framework=4.0 } "Running nunit test '$runCommand' failed."
}

Function Invoke-Nuget () {
	Write-Host "nuget.exe pack $nugetSpecFile -OutputDirectory $buildPublishDir -NoPackageAnalysis"
	& "$nugetRunnerDir\nuget.exe" pack $nugetSpecFile -OutputDirectory $buildPublishDir -NoPackageAnalysis
}

Function Get-TestFileName ( [string] $outputDir, [string] $runCommand ){
    $fileName = $runCommand -replace "\.", "-"
    return "$outputDir\$fileName"
}