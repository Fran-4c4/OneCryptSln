# Contributing to OneCrypt

Contributions should be small, reviewable and accompanied by evidence that the
affected behavior still builds and works.

## Before making a change

1. Read [SECURITY.md](SECURITY.md) and [the security design](docs/security.md).
2. Do not use real user files, credentials, certificates or private keys.
3. Check the origin and license before adding code, assets or dependencies.
4. Add tests for behavior changes, especially cryptography, file replacement,
   storage and Explorer integration.
5. Run `scripts/Build-Legacy.ps1` and
   `scripts/Test-PublicationReadiness.ps1`.

## Security-sensitive changes

Cryptographic formats are public contracts. A change must document its version,
algorithm suite, KDF parameters, limits, failure behavior and the owner of each
default. Include negative tests for wrong passwords, truncated or modified
data, unsupported versions and resource limits.

Do not add compatibility behavior for undocumented formats without an explicit,
tested product requirement.

## Repository hygiene

Generated outputs, deployed snapshots and downloaded binaries do not belong in
source control. Test fixtures must be synthetic and small. Never include secret
values in issues, logs, test output, commit messages or documentation.

The repository cannot produce redistributable releases until its license and
third-party obligations are resolved.
