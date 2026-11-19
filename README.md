# OneCrypt

OneCrypt is a Windows desktop application for organizing and encrypting files,
maintaining encrypted libraries and storing password records. The application
targets .NET Framework
4.8.

> [!WARNING]
> OneCrypt currently has no supported release and has not completed a modern
> cryptographic or application-security audit. Do not use it to protect
> sensitive information.

## Features

- file and text encryption workflows;
- encrypted-library organization;
- a local password-record store;
- English and Spanish user-interface resources;
- optional Windows Explorer context-menu and icon-handler components.

Update notifications use a configured HTTPS feed and never execute downloaded
code. The optional global keyboard shortcut is implemented in source and can
restore the application after two quick Control-key releases.

## Build

Requirements:

- Windows;
- Visual Studio 2022 Build Tools with the .NET desktop build tools workload;
- .NET Framework 4.8 Developer Pack;
- PowerShell 7 or Windows PowerShell 5.1;
- internet access for the first NuGet restore.

From the repository root, run:

```powershell
.\scripts\Build-Legacy.ps1
```

The script restores packages, creates a synthetic empty SQL Server Compact
database when necessary and builds `OneCryptSolution.sln` in Release/Any CPU.
The application is written to `OneCryptUI\bin\anycpu\Release\OneCrypt.exe`.

There is no supported installer at present. Run the built executable directly
from its output directory.

## Tests

Run the reproducible functional checks with:

```powershell
.\scripts\Test-OneCrypt.ps1
```

The suite builds the solution, tests the double-Control shortcut detector,
validates the HTTPS update-feed contract, performs an encrypt/decrypt round
trip through the Explorer menu implementation and loads both SharpShell COM
servers without changing the Windows Registry.

## Repository layout

- `OneCryptUI/` — Windows Forms executable and local data access.
- `OneCryptCore/` — cryptography, settings, logging and shared services.
- `OneCryptLang/` — localized resources.
- `OneCryptCtxExplorer/` — Explorer context-menu extension.
- `OneCryptCtxInstaller/` — Explorer-extension registration utility.
- `OneCryptIconHandler/` — Explorer icon handler.
- `WinAutoType/KeePassLib/` — auto-type support used by the password manager.
- `docs/` — product architecture, configuration and security documentation.
- `scripts/` — build and repository validation tools.

See [architecture](docs/architecture.md),
[configuration](docs/configuration.md), [security design](docs/security.md),
[contributing](CONTRIBUTING.md), [security policy](SECURITY.md) and
[third-party notices](THIRD_PARTY_NOTICES.md).

## Publication check

Before publishing or packaging the source tree, run:

```powershell
.\scripts\Test-PublicationReadiness.ps1
```

## License

An open-source license has not yet been selected. Do not redistribute the
repository or its binaries until ownership and third-party license
compatibility have been verified and a root `LICENSE` file has been added.
