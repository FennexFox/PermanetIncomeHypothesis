# Permanent Income Hypothesis

Project handoff and implementation brief for a separate CSL2 mod project.

## Goal

Rebuild household income and housing affordability around a Permanent Income Hypothesis-inspired model.

Vanilla household decisions are still dominated by labor income and short-run cash balances. The mod should introduce a broader concept of effective household means so that wealthy households in high-rent housing are no longer systematically misclassified as poor.

## Recommended Mod Name

- Primary: `Permanent Income Hypothesis`
- Short title option: `Permanent Income`
- Subtitle option: `PIH: Friedman's Take`

## Confirmed Vanilla Behavior

### 1. Vanilla does not use labor income only

`EconomyUtils.GetHouseholdIncome(...)` includes:

- wages
- residential tax above the minimum-earnings floor
- family allowance
- pension
- unemployment benefit

Code:

- [EconomyUtils.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Economy\EconomyUtils.cs#L428)

### 2. There is still no asset-based household income model

I did not find an explicit household asset ledger, real-estate ownership ledger, or capital-income calculation for households.

Household "wealth" is effectively:

- `household.m_Resources`
- plus `Resource.Money` in the household resource buffer

Code:

- [Household.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Citizens\Household.cs#L8)
- [EconomyUtils.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Economy\EconomyUtils.cs#L404)

### 3. Vanilla has one extra non-wage income path: company dividends

`CompanyDividendSystem` distributes company cash to the households of employees.

Important limitation:

- this is not ownership-based capital income
- it is effectively employee-linked profit sharing

Code:

- [CompanyDividendSystem.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\CompanyDividendSystem.cs#L121)

### 4. Household wealth categories are money thresholds, not true net worth

The UI wealth key is based on money thresholds only.

Code:

- [CitizenUIUtils.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\UI\InGame\CitizenUIUtils.cs#L69)
- [CitizenHappinessPrefab.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Prefabs\CitizenHappinessPrefab.cs#L61)

### 5. Rent is structurally driven, not household-specific

Per-renter asking rent is derived from:

- zone type
- building level
- lot size
- land value
- space multiplier
- property count

Code:

- [PropertyUtils.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Buildings\PropertyUtils.cs#L384)

### 6. Rent is directly deducted from household money

`PropertyRenterSystem` subtracts rent and garbage fee directly from household money.

Code:

- [PropertyRenterSystem.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\PropertyRenterSystem.cs#L121)

### 7. Housing choice is not price-aware at the scoring stage

`PropertyUtils.GetPropertyScore(...)` scores properties using:

- service availability
- apartment quality
- tax bonuses
- commute
- shelter penalty

There is no direct rent term in the score.

Code:

- [PropertyUtils.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Buildings\PropertyUtils.cs#L434)

### 8. Housing affordability is enforced through income gates

The actual affordability checks happen outside the property score.

#### Candidate home filtering

`CitizenPathfindSetup` only considers homes when:

- `askingRent + garbage fee <= householdIncome * density multiplier`
- support-eligible households are partially exempt

Code:

- [CitizenPathfindSetup.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\CitizenPathfindSetup.cs#L653)
- [CitizenPathfindSetup.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\CitizenPathfindSetup.cs#L664)

#### Final home selection

`HouseholdFindPropertySystem` uses income again:

- new rental requires `askingRent < income` unless support-eligible
- if staying in the current home and `income < currentRent`, the household can move away for `NoMoney`

Code:

- [HouseholdFindPropertySystem.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\HouseholdFindPropertySystem.cs#L584)
- [HouseholdFindPropertySystem.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\HouseholdFindPropertySystem.cs#L589)

#### Ongoing rent pressure / high-rent logic

`RentAdjustSystem` evaluates affordability using:

- `household income + positive cash on hand`

It uses that value to:

- trigger `PropertySeeker`
- count households toward the high-rent warning

Code:

- [RentAdjustSystem.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\RentAdjustSystem.cs#L297)
- [RentAdjustSystem.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\RentAdjustSystem.cs#L313)

### 9. Poverty / no-money behavior is tied to short-run resources

`HouseholdBehaviorSystem` stores daily income in `m_SalaryLastDay` and can trigger `MoveAwayReason.NoMoney` if:

- `householdTotalWealth + income < -1000`

Code:

- [HouseholdBehaviorSystem.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\HouseholdBehaviorSystem.cs#L168)
- [HouseholdBehaviorSystem.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Simulation\HouseholdBehaviorSystem.cs#L170)

### 10. Update order matters

Relevant order in `SystemOrder`:

- `HouseholdBehaviorSystem`
- `HouseholdFindPropertySystem`
- `PayWageSystem`
- `GameModeWealthSupportSystem`
- `BuildingUpkeepSystem`
- `RentAdjustSystem`
- `PropertyRenterSystem`
- `CompanyDividendSystem`

Implication:

- if a mod only adds money late in the tick, it will not fully fix the housing decision logic that already ran earlier

Code:

- [SystemOrder.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Common\SystemOrder.cs#L364)
- [SystemOrder.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Common\SystemOrder.cs#L394)
- [SystemOrder.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Common\SystemOrder.cs#L476)
- [SystemOrder.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Common\SystemOrder.cs#L488)
- [SystemOrder.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Common\SystemOrder.cs#L495)
- [SystemOrder.cs](C:\Users\techn\source\repos\FennexFox\CSL2_Decompiled\assemblies\Game\Game\Common\SystemOrder.cs#L498)

## Main Design Conclusion

The original hypothesis was only partly correct.

Vanilla households are not strictly wage-only, but the systems that matter for housing and poverty are still strongly centered on current labor-like income and cash flow. There is no robust household asset model. As a result, high-wealth households in expensive homes can be forced into behavior that looks unrealistically poor because the simulation does not distinguish well between:

- current labor income
- transfers
- wealth stock
- wealth-derived income
- long-run sustainable consumption

## Recommended Mod Design

Do not implement this as "just add free money every day."

Implement two distinct concepts:

### 1. Permanent income

Define an `effective household income` used for decision-making.

Suggested components:

- labor income
- transfer income
- employee profit-sharing if desired
- wealth-derived income
- optional smoothing from recent income history

### 2. Liquid wealth / buffer stock

Keep a distinction between:

- ongoing permanent income
- liquid buffer available for shocks and rent spikes

This avoids turning the mod into a pure cash faucet.

## Minimum Viable Implementation

If the new project needs a first playable version, target these pieces first.

### A. New household effective-income calculation

Create a central helper or system that computes:

- base labor and transfer income
- plus wealth-derived supplement
- optional smoothing term

This should become the mod's canonical household income signal.

### B. Patch all major household affordability call sites

These are the core systems to replace or intercept:

- `HouseholdBehaviorSystem`
- `CitizenPathfindSetup`
- `HouseholdFindPropertySystem`
- `RentAdjustSystem`
- optional: `SicknessCheckSystem`

Reason:

- these systems directly decide home access, rent pressure, and no-money behavior

### C. Add a late money-transfer system only as accounting support

A separate payout system can keep actual household money roughly aligned with the new model, but it should not be the only intervention.

## Suggested Wealth-Derived Income Model

Because vanilla does not expose true household-owned capital, use a proxy model.

Recommended conservative version:

- derive capital income from household liquid wealth
- apply a low annualized return converted to the game's update cadence
- cap the contribution so it supplements wages rather than replaces them
- optionally scale by household education, age mix, or district land value if a stronger class structure is desired

Recommended anti-exploit rules:

- do not make return purely proportional to current cash with no damping
- do not directly tie extra income to current rent level
- do not let high-rent households earn more just because they already occupy expensive housing

## Suggested Formula Direction

Example high-level form:

`effectiveIncome = laborIncome + transfers + employeeProfitShare + wealthIncome + smoothedIncomeAdjustment`

Where:

- `wealthIncome` is a mild annuity-like term from liquid wealth
- `smoothedIncomeAdjustment` can use trailing household income or trailing wealth changes

For a first pass, keep it simple:

`wealthIncome = clamp(liquidWealth * returnRatePerDay, 0, wealthIncomeCap)`

## Expected Gameplay Effects

If implemented carefully:

- fewer false `NoMoney` move-outs in affluent neighborhoods
- fewer spurious high-rent warnings in low-density and luxury housing
- more stable upscale residential sorting
- more believable gap between truly poor households and temporarily cash-constrained wealthy ones
- more realistic consumption and leisure behavior

If overtuned:

- rich households can compound upward too fast
- luxury residential can become overstable
- rent pressure may increase if purchasing power rises faster than supply
- inequality can become stronger than intended

## Recommended Development Order

1. Create separate mod project scaffold.
2. Add a design document defining `effective income`, `wealth income`, and balancing constants.
3. Implement shared calculation code.
4. Patch housing-affordability systems first.
5. Add actual household payout/accounting pass.
6. Add debug UI or logging for:
   - labor income
   - transfers
   - wealth income
   - effective income
   - rent burden
7. Balance with test cities:
   - low-density rich suburb
   - mixed mid-density city
   - high-unemployment city
8. Only after stabilization, consider optional advanced features.

## Nice-to-Have Features Later

- district-based wealth effects
- age-sensitive drawdown behavior for retirees
- inheritance / intergenerational transfer proxy
- education-dependent portfolio return proxy
- configurable ideology presets:
  - conservative PIH
  - stronger wealth effects
  - egalitarian smoothing

## Open Technical Questions For The New Project

- Whether to Harmony-patch helper methods or replace full systems for Burst-driven callers
- How to expose mod settings:
  - wealth return rate
  - smoothing strength
  - affordability multiplier
  - payout cadence
- Whether to store additional per-household state for moving averages
- Whether to keep vanilla company dividends unchanged or fold them into the new permanent-income layer

## First Deliverables For The New Repo

- mod name and Steam-facing copy
- architecture note for the new income model
- patch plan per vanilla system
- debug instrumentation plan
- balancing spreadsheet or constants sheet

## Steam Long Description Draft Source

See the earlier draft built around:

- vanilla being too wage-centered for housing outcomes
- PIH-inspired long-run household means
- better handling of affluent households in high-rent homes
- no intention to trivialize the game with free money

This brief should be the baseline source for that write-up.
