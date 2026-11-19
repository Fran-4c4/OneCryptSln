# Configuration

OneCrypt does not require credentials, certificates or API keys to build or
start. Never add real secrets to `app.config`, project files or source control.

## Database

The default connection string in `OneCryptUI/app.config` is deliberately
portable:

```xml
<add name="My.MySettings.oneCryptDatabaseConnectionString"
     connectionString="Data Source=|DataDirectory|"
     providerName="Microsoft.SqlServerCe.Client.4.0" />
```

`|DataDirectory|` is resolved by the application at runtime. A new development
checkout receives an empty synthetic `OneCryptUI/Data/oneCryptDatabase.sdf`
when `scripts/Build-Legacy.ps1` runs. Configure another local database through
the application settings; do not commit that path or database.

## Logging

NLog is configured in `OneCryptUI/app.config`. Development logs are written
under the build output and are ignored by Git. Logs must not contain passwords,
plaintext, key material, personal information or complete connection strings.

## Optional features

### Update notifications

`OneCrypt.UpdateFeedUrl` in `OneCryptUI/app.config` owns the update channel.
Its empty default disables checks. A non-empty value must be an absolute HTTPS
URI returning at most 256 KiB with this contract:

```xml
<onecryptUpdate>
  <version>8.0.0</version>
  <downloadUrl>https://github.com/OWNER/REPOSITORY/releases/tag/v8.0.0</downloadUrl>
  <releaseNotes>Short release summary.</releaseNotes>
</onecryptUpdate>
```

The application only notifies and opens the HTTPS release page after user
confirmation; it never downloads or executes an installer.

### Keyboard shortcut and Explorer integration

The global shortcut restores OneCrypt when Control is released twice within
350 milliseconds. It can be disabled in the application options. Explorer
integration is installed separately with `OneCryptCtxInstaller` and is not
required to run the desktop application.
