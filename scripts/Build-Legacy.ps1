[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$solutionPath = Join-Path $repositoryRoot 'OneCryptSolution.sln'
$databaseInitializer = Join-Path $PSScriptRoot 'Initialize-LegacyDatabase.ps1'
$vswherePath = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'

if (-not (Test-Path -LiteralPath $vswherePath)) {
    throw 'Visual Studio Installer vswhere.exe was not found.'
}

$msbuildPath = & $vswherePath `
    -latest `
    -products * `
    -requires Microsoft.Component.MSBuild `
    -find 'MSBuild\**\Bin\MSBuild.exe' |
    Select-Object -First 1

if (-not $msbuildPath -or -not (Test-Path -LiteralPath $msbuildPath)) {
    throw 'MSBuild from Visual Studio 2022 Build Tools was not found.'
}

& $msbuildPath `
    $solutionPath `
    /t:Restore `
    /p:Configuration=Release `
    '/p:Platform=Any CPU' `
    /m `
    /nologo `
    /verbosity:minimal `
    '/clp:ErrorsOnly;Summary'
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

& $databaseInitializer
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

& $msbuildPath `
    $solutionPath `
    /t:Rebuild `
    /p:Configuration=Release `
    '/p:Platform=Any CPU' `
    /m `
    /nologo `
    /verbosity:minimal `
    '/clp:ErrorsOnly;Summary'

$buildExitCode = $LASTEXITCODE
if ($buildExitCode -ne 0) {
    exit $buildExitCode
}

& (Join-Path $PSScriptRoot 'Stage-ShellIntegration.ps1')
exit $LASTEXITCODE

