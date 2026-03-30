# Permanent Income Hypothesis

`Permanent Income Hypothesis` is a Cities: Skylines II mod scaffold focused on
replacing vanilla household affordability decisions with a broader view of
household means anchored in expected earning capacity rather than current flow
income alone.

The design baseline comes from
[PermanentIncomeHypothesis_ProjectBrief.md](./PermanentIncomeHypothesis_ProjectBrief.md).
The current primary design direction is captured in
[docs/discussions/vanilla-friendly-proxy-strategy.md](./docs/discussions/vanilla-friendly-proxy-strategy.md).
That discussion supersedes the earlier housing-floor intuition and shifts the
target implementation toward a transfer-preserving, attachment-gated expected
earnings anchor plus weak secondary signals and a low-frequency household
cache.

## Current Status

The repository is intentionally at scaffold stage:

- shared effective-income calculator, proxy estimators, and cache models are in place
- mod settings expose the first balancing controls
- patch targets are documented and represented in code as an explicit registry
- runtime systems now reserve the cache-update path without emitting synthetic household samples
- gameplay-changing Harmony patches are not implemented yet

## Planned Model

The intended implementation direction is now:

```text
effectiveIncome =
    max(observedBaseIncome, cappedStructuralAnchor)
  + smallVehicleBonus
  + boundedLiquidWealthBonus
```

See the design discussion for the rationale and performance model:
[docs/discussions/vanilla-friendly-proxy-strategy.md](./docs/discussions/vanilla-friendly-proxy-strategy.md).

High-level first-pass formula:

```text
effectiveIncome =
    max(observedBaseIncome, cappedStructuralAnchor)
  + smallVehicleBonus
  + boundedLiquidWealthBonus
```

For the current scaffold:

- housing is not used as an income inference source
- transfer income is preserved as a separate guaranteed anchor
- expected earnings are attachment-gated before they can rise above observed income
- vehicle and liquid-wealth effects are bounded supporting signals only
- household-level cache types exist before any patch consumer starts reading them
- affordability settings are separated from any future accounting/payout layer

## Repository Layout

```text
PIH/
  Constants/    default balance knobs and shared constants
  Core/         effective-income calculation helpers
  Debugging/    affordability snapshot models for logs and probes
  Models/       input/output data contracts for the income model
  Patches/      explicit patch-target registry for major vanilla call sites
  Systems/      bootstrap and diagnostics systems
docs/
  architecture/ model notes and runtime layout
  discussions/  design-direction notes and tradeoff discussions
  balancing/    initial constants sheet
.github/
  ISSUE_TEMPLATE/
  instructions/
  workflows/
```

## First Implementation Targets

- `HouseholdBehaviorSystem`
- `CitizenPathfindSetup`
- `HouseholdFindPropertySystem`
- `RentAdjustSystem`
- optional follow-up: `SicknessCheckSystem`

These are tracked in
[docs/architecture/patch-plan.md](./docs/architecture/patch-plan.md) and
[PIH/Patches/PatchPlanRegistry.cs](./PIH/Patches/PatchPlanRegistry.cs).

## Build Notes

- This project uses the Cities: Skylines II modding MSBuild toolchain.
- `CSII_TOOLPATH` must point at the local mod tools directory.
- GitHub tag pushes create a source-based GitHub Release for `v*` tags.
- Release builds and Paradox Mods publishing happen from a local maintainer
  environment via `scripts/release.ps1`.

## Key Docs

- Primary design discussion:
  [docs/discussions/vanilla-friendly-proxy-strategy.md](./docs/discussions/vanilla-friendly-proxy-strategy.md)
- Architecture note:
  [docs/architecture/effective-income-model.md](./docs/architecture/effective-income-model.md)
- Patch plan:
  [docs/architecture/patch-plan.md](./docs/architecture/patch-plan.md)
- Debug instrumentation plan:
  [docs/architecture/debug-instrumentation-plan.md](./docs/architecture/debug-instrumentation-plan.md)
- Runtime layout:
  [docs/architecture/runtime-layout.md](./docs/architecture/runtime-layout.md)
- Balance defaults:
  [docs/balancing/constants-sheet.md](./docs/balancing/constants-sheet.md)
- Contributor workflow:
  [CONTRIBUTING.md](./CONTRIBUTING.md)
- Maintainer workflow:
  [MAINTAINING.md](./MAINTAINING.md)
