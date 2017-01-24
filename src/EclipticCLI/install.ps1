try {
   New-PSDrive -Name HKCR -PSProvider Registry -Root HKEY_CLASSES_ROOT
} catch {
  #do nothing
}

$currentDir = resolve-path .

Push-Location
Set-Location hkcr:

New-Item -Path .\Directory\Shell -Name Specflow –Force
Set-Item -Path .\Directory\Shell\Specflow -Value "Generate Acceptance Tests"
New-Item -Path .\Directory\Shell\Specflow -Name command -Force
Set-Item -Path .\Directory\Shell\Specflow\command -Value "$currentDir\Ecliptic.exe %1"

Pop-Location