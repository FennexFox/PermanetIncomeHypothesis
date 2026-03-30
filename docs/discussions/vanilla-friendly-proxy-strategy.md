# Discussion Draft: Transfer-Preserving, Attachment-Gated Expected Earnings Anchor

## Why This Discussion Exists

The main design problem is not the Permanent Income Hypothesis itself. The
problem is that vanilla does not appear to expose a strong household asset
ledger that would let the mod estimate permanent income or total assets
directly.

That makes a pure `liquid wealth -> capital income` design tempting, but also
too narrow. Liquid wealth is visible, cheap to read, and useful, yet it is not
strong enough by itself to represent long-run household means.

This discussion sets the preferred direction for the repository going forward.

## Core Direction

The most vanilla-friendly solution is not to treat houses and cars as direct
assets. It is to treat stable earning capacity as the main anchor and use
housing only as a consistency check on the result.

That distinction matters:

- housing should be interpreted as a post-estimation consistency signal, not an
  input to income inference
- vehicles can be used as a small durable-goods signal, not as a primary wealth
  metric
- liquid wealth should remain a bounded supporting signal, not the main driver

## Why Housing Is Not The Anchor

Housing is still useful, but not as the main source of the estimate. A house
choice is already downstream of income, so using current rent as the primary
anchor risks circularity. The model should instead infer earning capacity from
slower-moving exogenous traits and use housing only to check whether the
resulting estimate is broadly consistent with observed burden.

## Why Vehicles Are Only A Weak Signal

Vehicles are still useful, but only as a small auxiliary proxy:

- they are closer to an owned durable than housing is
- they can help separate households with otherwise similar current income
- they are still a weak class signal because vanilla vehicle selection is not a
  clean market-price choice

The mod should therefore use simple, bounded adjustments such as:

- small bonus per owned vehicle
- optional slightly higher bonus for larger or faster personal vehicles
- no large jumps based on vehicle type alone

## Why Liquid Wealth Should Stay Bounded

Liquid wealth should not disappear from the model, but it should be demoted
from "main permanent-income engine" to "weak supporting signal."

Preferred shape:

- use a square-root, logarithmic, or otherwise damped mapping
- keep the bonus small relative to flow income and the structural anchor
- never tie the bonus directly to current rent

## Recommended Formula Direction

The preferred strategic direction is:

```text
effectiveIncome =
    max(observedBaseIncome, cappedStructuralAnchor)
  + smallVehicleBonus
  + boundedLiquidWealthBonus
```

For a more cache-oriented implementation, this can become:

```text
effectiveIncome =
    max(smoothedObservedIncome, cappedStructuralAnchor)
  + smallVehicleBonus
  + boundedLiquidWealthBonus
```

Interpretation:

- `currentIncome` remains the vanilla-compatible baseline
- `smoothedObservedIncome` prevents overreaction to short-run income shocks
- `cappedStructuralAnchor` is built from transfer-preserving and
  attachment-gated earnings signals
- `smallVehicleBonus` and `boundedLiquidWealthBonus` are secondary signals only

## Guardrails

The following rules should stay in force:

- do not estimate market asset value from the current home
- do not use housing as an input to the income estimate
- do not give large meaning to vehicle type alone
- do not pay out `effectiveIncome - currentIncome` in full as cash
- use some of the gap for affordability logic only, and only a bounded portion
  for accounting alignment if needed

## Anti-Patterns To Avoid

- treating housing as the main source of permanent income
- granting large automatic income because current rent is high
- interpreting vehicle choice as a strong luxury-price signal
- recalculating the full proxy stack independently inside every vanilla call
  site

## Performance Direction

The expensive part is not "one household calculation exists." The expensive
part is "multiple systems keep recomputing similar household data separately."

This design is compatible with a low-cost cache-first approach.

### Preferred Runtime Shape

- keep state at the household level, not the citizen level
- add one shared cache component for permanent-income-related values
- update the cache once per in-game day, or in partitioned slices such as
  `1/32` of households per frame group
- refresh vehicle-derived values on vehicle-buffer changes or daily cadence
- make affordability systems read cached values instead of rebuilding the same
  signal repeatedly

### Suggested Cached Fields

- `EffectiveIncome`
- `CurrentIncome`
- `SmoothedObservedIncome`
- `GuaranteedTransferAnchor`
- `EarningsAnchorRaw`
- `AttachmentScore`
- `VehicleSignal`
- `LiquidWealthSignal`
- `HousingBurdenRatio`
- `HousingConsistencyState`
- `LastUpdatedDay`

This should remain practical because:

- household count is lower than citizen count
- vehicle counts are cheap if the signal stays coarse
- liquid money reads are cheap
- a shared cache can reduce repeated household-buffer scans across multiple
  systems

## Practical Patch Direction

The implementation sequence implied by this discussion is:

1. Build a household cache component and shared calculation pass.
2. Feed that cache into the high-value affordability systems first.
3. Use accounting payouts only as a secondary alignment layer.
4. Keep diagnostics focused on comparing vanilla income, capped structural
   anchor, cache output, housing burden, and final affordability decisions.

Priority readers of the cache remain:

- `CitizenPathfindSetup`
- `HouseholdFindPropertySystem`
- `RentAdjustSystem`
- `HouseholdBehaviorSystem`

## Relation To The Current Scaffold

The repository now includes:

- proxy-aligned `EffectiveIncomeInputs` and `EffectiveIncomeBreakdown` types
- bounded signal estimators for vehicles and liquid wealth
- a `HouseholdPermanentIncomeCache` model and cache factory
- a cache-update system placeholder that avoids synthetic household samples

The remaining work is no longer conceptual alignment. It is live integration:

- real household reads
- real cache writes
- first cache consumers in the housing-affordability systems

## Short Version

If the mod wants to stay vanilla-friendly, the best path is:

`transfer-preserving, attachment-gated expected earnings anchor + weak durable-goods signals + bounded liquid-wealth bonus + shared household cache`

That is a better fit than treating liquid wealth alone as synthetic capital
income, and it is also compatible with a practical performance budget.
