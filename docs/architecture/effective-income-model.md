# Effective Income Model

## Purpose

The mod introduces a canonical `effective household income` signal for
decision-making paths that are currently too dependent on current labor-like
income and short-run money balances.

## Current Design Direction

The main design direction is documented in
[../discussions/vanilla-friendly-proxy-strategy.md](../discussions/vanilla-friendly-proxy-strategy.md).

The short version is:

- use a transfer-preserving, attachment-gated expected earnings anchor
- treat vehicles as a weak durable-goods signal only
- keep liquid wealth as a bounded supporting signal
- treat housing as a post-estimation consistency diagnostic, not as an input to
  effective-income inference
- cache the result at household level and let multiple systems share it

The current code scaffold now carries explicit proxy-aligned inputs,
breakdowns, signal estimators, and household-cache types. Real household reads
and cache writes are still pending.

## First-Pass Components

- current vanilla income
- smoothed observed income
- guaranteed transfer anchor
- attachment-gated expected earnings anchor
- weak vehicle signal
- bounded liquid-wealth signal
- housing burden ratio and consistency state

## First-Pass Formula

```text
effectiveIncome =
    max(observedBaseIncome, cappedStructuralAnchor)
  + smallVehicleBonus
  + boundedLiquidWealthBonus
```

## Proxy Guardrails

- housing is not used as an income inference source
- housing is only used to classify post-estimation consistency
- vehicle signals stay weak and coarse
- liquid wealth stays bounded and damped
- affordability logic and payout logic remain distinct
- household cache updates should happen at low frequency rather than inside
  every consumer system

## Initial Repository Mapping

- defaults:
  [PIH/Constants/PihBalanceDefaults.cs](../../PIH/Constants/PihBalanceDefaults.cs)
- model contracts:
  [PIH/Models/EffectiveIncomeInputs.cs](../../PIH/Models/EffectiveIncomeInputs.cs),
  [PIH/Models/EffectiveIncomeBreakdown.cs](../../PIH/Models/EffectiveIncomeBreakdown.cs),
  [PIH/Models/HouseholdPermanentIncomeCache.cs](../../PIH/Models/HouseholdPermanentIncomeCache.cs)
- calculator:
  [PIH/Core/EffectiveIncomeCalculator.cs](../../PIH/Core/EffectiveIncomeCalculator.cs)
- signal estimators:
  [PIH/Core/ProxySignalEstimator.cs](../../PIH/Core/ProxySignalEstimator.cs)
- cache factory:
  [PIH/Core/HouseholdPermanentIncomeCacheFactory.cs](../../PIH/Core/HouseholdPermanentIncomeCacheFactory.cs)
- primary direction discussion:
  [../discussions/vanilla-friendly-proxy-strategy.md](../discussions/vanilla-friendly-proxy-strategy.md)
