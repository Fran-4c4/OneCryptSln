# OneCrypt repository rules

## Product boundary

- Keep this repository independently buildable and understandable. Do not add
  dependencies on private planning material.
- Product documentation describes the current product in present tense. Do not
  include repository audits, source comparisons, cleanup logs, discarded
  alternatives, conversion narratives or migration plans.
- Keep only documentation needed to build, configure, operate, integrate,
  secure, maintain and contribute to OneCrypt.

## Security product rules

- Treat all cryptographic behavior as security-critical.
- There is no compatibility requirement for existing encrypted files or
  databases. A new format must be versioned, authenticated and covered by
  synthetic fixtures and negative tests.
- Never commit private keys, certificates, credentials, production data,
  personal data or real encrypted user material.
- Never log passwords, derived keys, plaintext, authentication keys or complete
  connection strings.
- A security-sensitive default has one documented owner. Reject invalid
  configuration instead of silently substituting another setting.

## Publication and dependencies

- Publish only a verified clean tree or demonstrably scrubbed history.
- Do not add generated binaries, deployed snapshots or third-party executables
  to source control.
- Record the origin, version, license and consumer of every dependency.
- Do not declare a repository license until the KeePass-derived GPL code is
  retained under compatible terms or replaced completely.
- Run `scripts/Test-PublicationReadiness.ps1` before preparing a public tree.

## Build discipline

- Keep `OneCryptSolution.sln` buildable on .NET Framework 4.8 with
  `scripts/Build-Legacy.ps1`.
- Prefer package restore or retained source projects over copied DLLs.
- Use synthetic data and disposable fixtures for validation.
