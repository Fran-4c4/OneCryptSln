[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$applicationPath = Join-Path $repositoryRoot 'OneCryptUI\bin\anycpu\Release\OneCrypt.exe'

if (-not (Test-Path -LiteralPath $applicationPath)) {
    throw 'Build OneCrypt before running the keyboard shortcut test.'
}

$originalLocation = Get-Location
try {
    Set-Location (Split-Path -Parent $applicationPath)
    [void] [System.Reflection.Assembly]::LoadFrom($applicationPath)
    $detector = [ControlDoublePressDetector]::new()
    $millisecond = [TimeSpan]::TicksPerMillisecond

    if ($detector.RegisterKeyUp(0x11, 0)) {
        throw 'A single Control release must not activate the shortcut.'
    }
    if (-not $detector.RegisterKeyUp(0x11, 300 * $millisecond)) {
        throw 'Two Control releases within 350 ms must activate the shortcut.'
    }

    [void] $detector.RegisterKeyUp(0x11, 0)
    [void] $detector.RegisterKeyUp(0x41, 100 * $millisecond)
    if ($detector.RegisterKeyUp(0x11, 200 * $millisecond)) {
        throw 'Another key must reset the Control sequence.'
    }

    $detector.Reset()
    [void] $detector.RegisterKeyUp(0x11, 0)
    if ($detector.RegisterKeyUp(0x11, 351 * $millisecond)) {
        throw 'Control releases outside the 350 ms window must not activate the shortcut.'
    }

    Write-Host 'Keyboard shortcut detector tests passed.' -ForegroundColor Green
}
finally {
    Set-Location $originalLocation
}
