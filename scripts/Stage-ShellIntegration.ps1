[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$applicationOutput = Join-Path $repositoryRoot 'OneCryptUI\bin\anycpu\Release'
$artifacts = @(
    @{ Source = 'OneCryptCtxExplorer\bin\anycpu\Release\OneCryptCtxExplorer.dll'; Name = 'OneCryptCtxExplorer.dll' },
    @{ Source = 'OneCryptIconHandler\bin\Release\OneCryptIconHandler.dll'; Name = 'OneCryptIconHandler.dll' },
    @{ Source = 'OneCryptCtxInstaller\bin\Release\OneCryptCtxInstaller.exe'; Name = 'OneCryptCtxInstaller.exe' }
)

if (-not (Test-Path -LiteralPath $applicationOutput)) {
    throw "Application output does not exist: $applicationOutput"
}

foreach ($artifact in $artifacts) {
    $source = Join-Path $repositoryRoot $artifact.Source
    if (-not (Test-Path -LiteralPath $source)) {
        throw "Shell integration build output does not exist: $source"
    }
    Copy-Item -LiteralPath $source -Destination (Join-Path $applicationOutput $artifact.Name) -Force
}

Write-Host "Staged Explorer integration in: $applicationOutput"
