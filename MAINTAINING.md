# Maintaining

## Release Baseline

- Treat `PIH/Properties/PublishConfiguration.xml` as the single source of truth
  for the shipped mod name, version, public description, and changelog.
- Keep README, release notes, and publish configuration aligned.
- Do not describe investigational behavior as a confirmed fix.

## GitHub Automation

- `ci.yml` validates the repository scaffold and builds only when the repository
  variable `CSII_TOOLPATH` is configured.
- `release.yml` creates a GitHub Release when a `v*` tag is pushed.
- Use `scripts/release.ps1` for local release builds, Paradox Mods publishing,
  and release tag push orchestration.

## When Balance Or Patch Scope Changes

- Update:
  - [docs/balancing/constants-sheet.md](./docs/balancing/constants-sheet.md)
  - [docs/architecture/effective-income-model.md](./docs/architecture/effective-income-model.md)
  - [docs/architecture/patch-plan.md](./docs/architecture/patch-plan.md)
- Check whether the README and publish long description still describe the
  current implementation honestly.

## Versioning

- Use semantic versions while the repo is still pre-release.
- Keep `AssemblyInformationalVersion` derived from
  `PublishConfiguration.xml` so runtime logs and release metadata stay aligned.

