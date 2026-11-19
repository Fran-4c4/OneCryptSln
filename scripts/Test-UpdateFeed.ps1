[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$applicationPath = Join-Path $repositoryRoot 'OneCryptUI\bin\anycpu\Release\OneCrypt.exe'

if (-not (Test-Path -LiteralPath $applicationPath)) {
    throw 'Build OneCrypt before running the update feed test.'
}

$originalLocation = Get-Location
try {
    Set-Location (Split-Path -Parent $applicationPath)
    [void] [System.Reflection.Assembly]::LoadFrom($applicationPath)

    if ($null -ne [frms.autoupdate.AutoUpdate]::ParseConfiguredFeedUri('')) {
        throw 'The empty update-feed setting must disable update checks.'
    }
    $configuredUri = [frms.autoupdate.AutoUpdate]::ParseConfiguredFeedUri(
        'https://example.invalid/onecrypt-update.xml'
    )
    if ($configuredUri.Scheme -ne 'https') {
        throw 'An explicit HTTPS update-feed setting must be accepted.'
    }
    $httpSettingRejected = $false
    try {
        [void] [frms.autoupdate.AutoUpdate]::ParseConfiguredFeedUri(
            'http://example.invalid/onecrypt-update.xml'
        )
    }
    catch {
        $httpSettingRejected = $true
    }
    if (-not $httpSettingRejected) {
        throw 'An HTTP update-feed setting must be rejected.'
    }

    $validFeed = @'
<onecryptUpdate>
  <version>8.0.0</version>
  <downloadUrl>https://example.invalid/releases/v8.0.0</downloadUrl>
  <releaseNotes>Synthetic test release.</releaseNotes>
</onecryptUpdate>
'@
    $release = [frms.autoupdate.AutoUpdate]::ParseUpdateFeed($validFeed)
    if ($release.Version.ToString() -ne '8.0.0' -or
        $release.DownloadUri.Scheme -ne 'https') {
        throw 'The valid update feed was not parsed correctly.'
    }

    $unsafeFeed = $validFeed.Replace('https://', 'http://')
    $unsafeRejected = $false
    try {
        [void] [frms.autoupdate.AutoUpdate]::ParseUpdateFeed($unsafeFeed)
    }
    catch {
        $unsafeRejected = $true
    }
    if (-not $unsafeRejected) {
        throw 'An HTTP release URL must be rejected.'
    }

    Write-Host 'Update feed contract tests passed.' -ForegroundColor Green
}
finally {
    Set-Location $originalLocation
}
