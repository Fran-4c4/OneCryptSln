[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$outputDirectory = Join-Path $repositoryRoot 'OneCryptUI\bin\anycpu\Release'
$extensionPath = Join-Path $outputDirectory 'OneCryptCtxExplorer.dll'

if (-not (Test-Path -LiteralPath $extensionPath)) {
    throw 'Build OneCrypt before running the shell operations test.'
}

$tempRoot = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath()).TrimEnd('\')
$testDirectory = [System.IO.Path]::GetFullPath(
    (Join-Path $tempRoot ('OneCrypt-ShellTest-' + [Guid]::NewGuid().ToString('N')))
)
if (-not $testDirectory.StartsWith($tempRoot + '\', [StringComparison]::OrdinalIgnoreCase)) {
    throw 'The shell test directory must remain inside the system temporary directory.'
}

$originalLocation = Get-Location
try {
    [void] [System.IO.Directory]::CreateDirectory($testDirectory)
    $plainPath = Join-Path $testDirectory 'sample.txt'
    $encryptedPath = $plainPath + '.ocpt'
    $expectedContent = 'Synthetic OneCrypt shell integration test.'
    [System.IO.File]::WriteAllText($plainPath, $expectedContent)

    Set-Location $outputDirectory
    $assembly = [System.Reflection.Assembly]::LoadFrom($extensionPath)
    $menuType = $assembly.GetType('onecrypt.OneCryptContextMenu', $true)
    $menu = [System.Activator]::CreateInstance($menuType)
    $passwordField = $menuType.GetField(
        'pwdForDecrytp',
        [System.Reflection.BindingFlags]'Instance,NonPublic'
    )
    if ($null -eq $passwordField) {
        throw 'Unable to locate the shell password field for the controlled test.'
    }
    $errorField = $menuType.GetField(
        'errorList',
        [System.Reflection.BindingFlags]'Instance,NonPublic'
    )
    $errorType = $assembly.GetType('ind.tools.error.ErrorVisorHelper', $false)
    if ($null -eq $errorType) {
        $errorType = [System.Reflection.Assembly]::LoadFrom(
            (Join-Path $outputDirectory 'OneCryptCore.dll')
        ).GetType('ind.tools.error.ErrorVisorHelper', $true)
    }
    $errors = [System.Activator]::CreateInstance($errorType)
    $errorField.SetValue($menu, $errors)

    $passwordField.SetValue($menu, 'synthetic-test-password')
    $menu.encryptFiles([string[]] @($plainPath))
    if ((Test-Path -LiteralPath $plainPath) -or -not (Test-Path -LiteralPath $encryptedPath)) {
        if ($errors.errorList.Count -gt 0) {
            throw ('Shell encryption failed: ' + $errors.errorList[0].getErrMsg())
        }
        throw 'The shell encryption operation did not produce the expected file state.'
    }

    $passwordField.SetValue($menu, 'synthetic-test-password')
    $menu.decryptFiles([string[]] @($encryptedPath), $false)
    if (-not (Test-Path -LiteralPath $plainPath) -or (Test-Path -LiteralPath $encryptedPath)) {
        throw 'The shell decryption operation did not produce the expected file state.'
    }
    if ([System.IO.File]::ReadAllText($plainPath) -ne $expectedContent) {
        throw 'The shell encryption round trip changed the synthetic file content.'
    }

    Write-Host 'Shell encryption and decryption tests passed.' -ForegroundColor Green
}
finally {
    Set-Location $originalLocation
    if (Test-Path -LiteralPath $testDirectory) {
        Remove-Item -LiteralPath $testDirectory -Recurse -Force
    }
}
