[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))

if (-not (Test-Path -LiteralPath (Join-Path $repositoryRoot '.git'))) {
    throw "The script must run from a Git checkout of OneCrypt."
}

$trackedFiles = @(
    git -C $repositoryRoot ls-files --cached --others --exclude-standard |
        Where-Object { Test-Path -LiteralPath (Join-Path $repositoryRoot $_) }
)
if ($LASTEXITCODE -ne 0) {
    throw "Unable to enumerate tracked files."
}

$failures = [System.Collections.Generic.List[object]]::new()

function Add-Failure {
    param(
        [Parameter(Mandatory)] [string] $Category,
        [Parameter(Mandatory)]
        [AllowEmptyCollection()]
        [string[]] $Paths
    )

    $normalizedPaths = @($Paths | Where-Object { $_ } | Sort-Object -Unique)
    if ($normalizedPaths.Count -eq 0) {
        return
    }

    $failures.Add([pscustomobject]@{
        Category = $Category
        Paths = $normalizedPaths
    })
}

$requiredFiles = @(
    'README.md',
    'CONTRIBUTING.md',
    'SECURITY.md',
    'THIRD_PARTY_NOTICES.md',
    'docs/architecture.md',
    'docs/security.md'
)

$missingRequiredFiles = @(
    $requiredFiles | Where-Object {
        -not (Test-Path -LiteralPath (Join-Path $repositoryRoot $_))
    }
)
Add-Failure -Category 'Missing required publication documentation' -Paths $missingRequiredFiles

$sensitiveExtensions = @('.key', '.pem', '.pvk', '.pfx', '.p12', '.crt', '.cer')
$trackedSensitiveFiles = @(
    $trackedFiles | Where-Object {
        $sensitiveExtensions -contains [System.IO.Path]::GetExtension($_).ToLowerInvariant()
    }
)
Add-Failure -Category 'Tracked keys or certificates' -Paths $trackedSensitiveFiles

$generatedBinaryExtensions = @('.dll', '.exe', '.msi', '.cab', '.pdb')
$trackedGeneratedBinaries = @(
    $trackedFiles | Where-Object {
        $generatedBinaryExtensions -contains [System.IO.Path]::GetExtension($_).ToLowerInvariant()
    }
)
Add-Failure -Category 'Tracked generated or vendored binaries' -Paths $trackedGeneratedBinaries

$trackedSigningKeys = @(
    $trackedFiles | Where-Object {
        [System.IO.Path]::GetExtension($_).Equals('.snk', [System.StringComparison]::OrdinalIgnoreCase)
    }
)
Add-Failure -Category 'Tracked strong-name key material requiring an explicit decision' -Paths $trackedSigningKeys

$textExtensions = @(
    '.cs', '.vb', '.config', '.xml', '.settings', '.wxs', '.wixproj',
    '.csproj', '.vbproj', '.props', '.targets', '.ps1', '.md'
)
$textFiles = @(
    $trackedFiles | Where-Object {
        $textExtensions -contains [System.IO.Path]::GetExtension($_).ToLowerInvariant()
    }
)

$sensitiveSourcePatterns = @(
    '(?i)X509Certificate2\s*\([^\r\n]*\.pfx',
    '(?i)[A-Z]:\\(?:Users|Indron)\\',
    '(?i)http://www\.onecrypt\.info'
)

$sourceFindings = [System.Collections.Generic.List[string]]::new()
foreach ($relativePath in $textFiles) {
    $absolutePath = Join-Path $repositoryRoot $relativePath
    if (-not (Test-Path -LiteralPath $absolutePath)) {
        continue
    }

    $fileInfo = Get-Item -LiteralPath $absolutePath
    if ($fileInfo.Length -gt 2MB) {
        $sourceFindings.Add("$relativePath [not scanned: file exceeds 2 MiB]")
        continue
    }

    $content = [System.IO.File]::ReadAllText($absolutePath)
    foreach ($pattern in $sensitiveSourcePatterns) {
        if ([System.Text.RegularExpressions.Regex]::IsMatch($content, $pattern)) {
            $sourceFindings.Add($relativePath)
            break
        }
    }
}
Add-Failure -Category 'Sensitive local paths, implicit certificate loading or retired HTTP endpoint in source' -Paths @($sourceFindings)

if (-not (Test-Path -LiteralPath (Join-Path $repositoryRoot 'LICENSE'))) {
    Add-Failure -Category 'Repository license has not been selected' -Paths @('LICENSE')
}

if ($failures.Count -eq 0) {
    Write-Host 'Publication readiness checks passed.' -ForegroundColor Green
    exit 0
}

Write-Host "Publication readiness checks failed in $($failures.Count) categories." -ForegroundColor Red
foreach ($failure in $failures) {
    Write-Host "`n[$($failure.Category)] $($failure.Paths.Count) item(s)" -ForegroundColor Yellow
    $displayLimit = 20
    foreach ($path in @($failure.Paths | Select-Object -First $displayLimit)) {
        Write-Host " - $path"
    }
    if ($failure.Paths.Count -gt $displayLimit) {
        Write-Host " - ... and $($failure.Paths.Count - $displayLimit) more"
    }
}

exit 1

