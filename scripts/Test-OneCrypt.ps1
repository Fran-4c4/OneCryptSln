[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'

& (Join-Path $PSScriptRoot 'Build-Legacy.ps1')
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

foreach ($testName in @(
    'Test-KeyboardShortcut.ps1',
    'Test-UpdateFeed.ps1',
    'Test-ShellOperations.ps1'
)) {
    & (Join-Path $PSScriptRoot $testName)
    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }
}

$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$outputDirectory = Join-Path $repositoryRoot 'OneCryptUI\bin\anycpu\Release'
$installer = Join-Path $outputDirectory 'OneCryptCtxInstaller.exe'
$verification = Start-Process `
    -FilePath $installer `
    -ArgumentList 'verify' `
    -WorkingDirectory $outputDirectory `
    -WindowStyle Hidden `
    -Wait `
    -PassThru
if ($verification.ExitCode -ne 0) {
    throw "Explorer extension verification failed with exit code $($verification.ExitCode)."
}

Write-Host 'OneCrypt functional checks passed.' -ForegroundColor Green
