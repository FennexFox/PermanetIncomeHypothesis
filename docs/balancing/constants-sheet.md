# Constants Sheet

Initial scaffold defaults are intentionally conservative.

| Constant | Default | Notes |
| --- | --- | --- |
| `NonWorkerPotentialIncomeSharePercent` | `40` | Share of education-based potential income used for adult non-worker households. |
| `StudentPotentialIncomeSharePercent` | `20` | Share of education-based potential income used for adult student households. |
| `StructuralAnchorAdditiveFloor` | `25` | Small buffer above observed income before the earnings anchor is treated as overly aggressive. |
| `StructuralAnchorRelativeMarginPercent` | `30` | Relative margin above observed income before attachment dampening is applied. |
| `IncomeSmoothingDays` | `30` | Slower-moving observed-income layer for PIH-style persistence. |
| `VehicleSignalPerVehicle` | `25` | Weak durable-goods signal only. |
| `LiquidWealthSignalSqrtMultiplier` | `0.2` | Damped liquid-wealth support signal. |
| `LiquidWealthSignalCap` | `150` | Keeps liquid wealth secondary to the structural anchor. |
| `AffordabilityMultiplierPercent` | `100` | Neutral baseline for rent gate comparisons. |
| `CacheUpdatePartitions` | `32` | Default low-frequency household cache rotation. |
| `DiagnosticsSamplesPerDay` | `2` | Light default cadence when diagnostics are enabled. |

Removed housing-floor controls:

- `HousingFloorActivationTenureDays`
- `HousingFloorTargetBurdenPercent`
- `HousingFloorCapMultiplierX100`

Source of truth in code:
[PIH/Constants/PihBalanceDefaults.cs](../../PIH/Constants/PihBalanceDefaults.cs)
