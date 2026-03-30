# Patch Plan

## Objective

Patch the affordability and move-out call sites that actually govern household
housing behavior instead of only adding late-tick money transfers.

## Priority Targets

| Target | Priority | Why it matters |
| --- | --- | --- |
| `HouseholdBehaviorSystem` | High | Drives `NoMoney` behavior and short-run household distress. |
| `CitizenPathfindSetup` | High | Filters candidate homes before scoring based on affordability gates. |
| `HouseholdFindPropertySystem` | High | Re-checks affordability during final rental/home selection. |
| `RentAdjustSystem` | High | Drives high-rent pressure and property-seeker activation. |
| `SicknessCheckSystem` | Medium | Secondary follow-up if health-side affordability still assumes vanilla income. |

## Implementation Stages

1. Keep the calculator and settings stable.
2. Implement one patch at a time, starting with the housing-affordability
   filters.
3. Add diagnostics that reveal effective income, liquid reserve, and rent
   burden around each patched decision.
4. Only after those paths are stable, add any accounting or payout alignment
   system.

## Code Registry

The current scaffold mirrors this plan in
[PIH/Patches/PatchPlanRegistry.cs](../../PIH/Patches/PatchPlanRegistry.cs).