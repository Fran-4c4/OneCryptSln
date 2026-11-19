[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'
$repositoryRoot = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$outputPath = [System.IO.Path]::GetFullPath(
    (Join-Path $repositoryRoot 'OneCryptUI\Data\oneCryptDatabase.sdf')
)
$nugetRoot = if ($env:NUGET_PACKAGES) {
    $env:NUGET_PACKAGES
}
else {
    Join-Path ([Environment]::GetFolderPath('UserProfile')) '.nuget\packages'
}
$providerPath = [System.IO.Path]::GetFullPath((Join-Path $nugetRoot `
    'microsoft.sqlserver.compact\4.0.8876.1\lib\net40\System.Data.SqlServerCe.dll'))

if (-not $outputPath.StartsWith(
    $repositoryRoot + [System.IO.Path]::DirectorySeparatorChar,
    [System.StringComparison]::OrdinalIgnoreCase
)) {
    throw 'The database output path must remain inside the OneCrypt repository.'
}

if (Test-Path -LiteralPath $outputPath) {
    Write-Host "Synthetic legacy database already exists: $outputPath"
    exit 0
}

if (-not (Test-Path -LiteralPath $providerPath)) {
    throw "SQL Server Compact provider not found: $providerPath"
}

Add-Type -LiteralPath $providerPath

$schemaStatements = @(
    @'
CREATE TABLE T_Files (
    id int NOT NULL PRIMARY KEY,
    Name nvarchar(260) NOT NULL,
    FileNamePathQualified nvarchar(500) NOT NULL,
    size float NULL,
    lastAccess datetime NULL,
    Computer nvarchar(100) NULL,
    fileNotfound bit NULL
)
'@,
    'CREATE UNIQUE INDEX UX_T_Files_Path ON T_Files (FileNamePathQualified)',
    @'
CREATE TABLE T_Library (
    id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    Name nvarchar(500) NULL,
    FileCount int NULL,
    LastAccessDate datetime NULL,
    CreationDate datetime NULL
)
'@,
    'CREATE UNIQUE INDEX UX_T_Library_Name ON T_Library (Name)',
    @'
CREATE TABLE T_Library_Files (
    libraryID int NULL,
    fileID int NULL
)
'@,
    'CREATE INDEX IX_T_Library_Files_Library ON T_Library_Files (libraryID)',
    'CREATE INDEX IX_T_Library_Files_File ON T_Library_Files (fileID)',
    @'
CREATE TABLE T_RowID (
    id int NOT NULL,
    rowKey int NULL
)
'@,
    'INSERT INTO T_RowID (id, rowKey) VALUES (1, 0)',
    @'
CREATE TABLE T_Config (
    id int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    KeyValue nvarchar(100) NOT NULL,
    KeyValueAsNumber float NULL,
    KeyValueAsString nvarchar(200) NULL
)
'@,
    'CREATE UNIQUE INDEX UX_T_Config_KeyValue ON T_Config (KeyValue)'
)

$connectionString = "Data Source=$outputPath"
$engine = $null
$connection = $null
$transaction = $null
$createdByThisRun = $false

try {
    $engine = [System.Data.SqlServerCe.SqlCeEngine]::new($connectionString)
    $engine.CreateDatabase()
    $createdByThisRun = $true

    $connection = [System.Data.SqlServerCe.SqlCeConnection]::new($connectionString)
    $connection.Open()
    $transaction = $connection.BeginTransaction()

    foreach ($statement in $schemaStatements) {
        $command = $connection.CreateCommand()
        try {
            $command.Transaction = $transaction
            $command.CommandText = $statement
            [void] $command.ExecuteNonQuery()
        }
        finally {
            $command.Dispose()
        }
    }

    $transaction.Commit()
    $transaction.Dispose()
    $transaction = $null
    $connection.Close()

    Write-Host "Created synthetic legacy database: $outputPath"
}
catch {
    if ($null -ne $transaction) {
        $transaction.Rollback()
    }
    if ($null -ne $connection) {
        $connection.Dispose()
    }
    if ($null -ne $engine) {
        $engine.Dispose()
    }
    if ($createdByThisRun -and (Test-Path -LiteralPath $outputPath)) {
        [System.IO.File]::Delete($outputPath)
    }
    throw
}
finally {
    if ($null -ne $transaction) {
        $transaction.Dispose()
    }
    if ($null -ne $connection) {
        $connection.Dispose()
    }
    if ($null -ne $engine) {
        $engine.Dispose()
    }
}

