# Runtime Layout

The current scaffold splits responsibilities into four layers:

## 1. Settings And Balance Defaults

- [PIH/Setting.cs](../../PIH/Setting.cs)
- [PIH/Constants/PihBalanceDefaults.cs](../../PIH/Constants/PihBalanceDefaults.cs)

These define the user-facing knobs and their conservative defaults. The current
direction keeps housing out of the income model and exposes controls for the
expected-earnings anchor, smoothing, secondary signals, and cache cadence.

## 2. Income Model Core

- [PIH/Models/EffectiveIncomeInputs.cs](../../PIH/Models/EffectiveIncomeInputs.cs)
- [PIH/Models/EffectiveIncomeBreakdown.cs](../../PIH/Models/EffectiveIncomeBreakdown.cs)
- [PIH/Models/HouseholdPermanentIncomeCache.cs](../../PIH/Models/HouseholdPermanentIncomeCache.cs)
- [PIH/Core/EffectiveIncomeCalculator.cs](../../PIH/Core/EffectiveIncomeCalculator.cs)
- [PIH/Core/ProxySignalEstimator.cs](../../PIH/Core/ProxySignalEstimator.cs)
- [PIH/Core/HouseholdPermanentIncomeCacheFactory.cs](../../PIH/Core/HouseholdPermanentIncomeCacheFactory.cs)

This is the canonical calculation and cache-construction layer that later
Harmony patches should call. Housing only contributes to a consistency view,
not to the income estimate itself.

## 3. Patch Registry

- [PIH/Patches/PatchPlanRegistry.cs](../../PIH/Patches/PatchPlanRegistry.cs)

This keeps the first-wave vanilla interception targets explicit and documented.

## 4. Diagnostics And Bootstrap Systems

- [PIH/Systems/HouseholdPermanentIncomeCacheSystem.cs](../../PIH/Systems/HouseholdPermanentIncomeCacheSystem.cs)
- [PIH/Systems/PermanentIncomeDiagnosticsSystem.cs](../../PIH/Systems/PermanentIncomeDiagnosticsSystem.cs)

These currently reserve the cache-update path and keep diagnostics explicit
about missing live integration rather than emitting synthetic sample data.
