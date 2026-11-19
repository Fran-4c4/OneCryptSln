# Architecture

## Overview

OneCrypt is a Windows Forms desktop application targeting .NET Framework 4.8.
The main executable coordinates file browsing, encryption operations, library
metadata and password records. Optional Windows Explorer components expose a
small subset of those workflows from the shell.

## Components

### Desktop application

`OneCryptUI` owns the application entry point, forms, user controls and
workflow coordination. It also owns the SQL Server Compact data access used by
the library and password-manager features.

### Shared services

`OneCryptCore` contains encryption services, settings, logging, file helpers
and inter-process communication. `OneCryptLang` contains the shared English
and Spanish resources.

`WinAutoType/KeePassLib` builds `Autotype.Keepass.dll`, which provides the
auto-type engine used by password-manager workflows. The application references
this project as source rather than consuming a copied binary.

### Windows Explorer integration

`OneCryptCtxExplorer` implements the SharpShell context menu.
`OneCryptIconHandler` implements the shell icon handler.
`OneCryptCtxInstaller` provides explicit registration and unregistration
operations for these optional components.

The build stages all three Explorer-integration artifacts beside
`OneCrypt.exe`. Registration is initiated from the application options and
requires Windows elevation; it is never performed as part of compilation or
tests.

## Runtime data

The application keeps user-specific settings in the standard .NET user-settings
location. Library and password metadata use a SQL Server Compact database. The
repository contains only a synthetic empty database template; a user's database
must remain outside source control.

Cryptographic operations are performed by `OneCryptCore`. UI and Explorer
components pass requests to the core and must not implement independent
cryptographic algorithms.

## Dependencies and build output

NuGet restore supplies NLog, SharpShell, ExpTreeLib and the SQL Server Compact
runtime. SQL Server Compact native files for x86 and x64 are copied beside the
application during the build. The Release build output is self-contained in
`OneCryptUI\bin\anycpu\Release` apart from the installed .NET Framework 4.8
runtime.

The repository currently builds the executable and optional Explorer
components. It does not provide a supported installer.
