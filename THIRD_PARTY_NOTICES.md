# Third-party notices

This inventory identifies third-party code used by the build. It is not a
substitute for the license notices required by a distributable release.

| Component | Version | Use |
| --- | --- | --- |
| KeePass Password Safe-derived code | source in `WinAutoType/KeePassLib` | Auto-type support; source headers state GPL version 2 or later |
| NLog | 3.0.0 | Application logging |
| SharpShell | 2.1.0 | Windows Explorer extensions |
| ExpTreeLib | 3.2.0 | Explorer-style UI controls |
| Microsoft SQL Server Compact | 4.0.8876.1 | Local library and password metadata |

Package dependencies are restored from NuGet and are not committed as copied
DLLs. Before a release, verify the source, license, redistribution terms and
required notices for every component and transitive dependency.

The former updater package, global keyboard-hook binary, copied 7-Zip
executable and WiX deployment snapshot are not dependencies of the source
build. Update notification and keyboard-shortcut behavior are implemented in
the OneCrypt source tree.
