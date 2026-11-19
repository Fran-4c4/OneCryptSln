# Security design

## Current limitations

The current file-encryption implementation uses `RijndaelManaged` with key and
IV material derived directly from password text. Its file format does not
provide modern password hardening or authenticated encryption. OneCrypt must
therefore be treated as experimental software and must not be used for
security-sensitive data.

Certificate-based file encryption is unavailable through the general file API
unless the caller supplies a certificate explicitly. The application contains
no bundled certificates, private keys or certificate passwords. Update checks
accept only a bounded HTTPS metadata feed and never download or execute a
release. The optional global keyboard shortcut is implemented in source.

## Requirements for security-sensitive changes

A replacement encrypted format must:

- have an unambiguous magic value and version;
- use authenticated encryption and authenticate before exposing plaintext;
- derive password keys with a reviewed password KDF and a random per-container
  salt;
- encode and validate the algorithm and KDF parameters needed to read it;
- use a cryptographically secure source for salts, nonces and keys;
- bind security-relevant header data to authentication;
- define size and work limits and reject malformed input safely;
- write through a separate temporary file and replace the destination only
  after the complete result is valid;
- include deterministic tests and negative tests using synthetic data only.

Security-sensitive defaults must have one documented owner. Invalid or
contradictory settings must be rejected rather than silently changed.

## Release gates

No security claim or binary release should be made until the encrypted-format
contract, threat model, dependency inventory, failure tests and publication
tree have received security review.
