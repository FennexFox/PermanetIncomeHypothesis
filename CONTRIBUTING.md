# Contributing

## Ground Rules

- Keep claims tied to observed vanilla behavior or reproducible mod behavior.
- Separate confirmed fixes from hypotheses, experiments, and instrumentation.
- Prefer small, reviewable changes that move one system or one balancing concern
  at a time.

## Before Opening A PR

- Update docs when changing patch scope, balance defaults, or public-facing
  claims.
- If you change the affordability model, update both the code defaults and
  [docs/balancing/constants-sheet.md](./docs/balancing/constants-sheet.md).
- If you add or remove a vanilla interception point, update
  [docs/architecture/patch-plan.md](./docs/architecture/patch-plan.md) and
  [PIH/Patches/PatchPlanRegistry.cs](./PIH/Patches/PatchPlanRegistry.cs).
- Run a local `dotnet build PIH.slnx` when the CSL2 toolchain is available.

## PR Expectations

- State what changed, why it changed, and what evidence supports it.
- Call out remaining risks, especially around Harmony patch fragility,
  update order, or balancing assumptions.
- Do not merge placeholder instrumentation or settings without documenting what
  they are expected to support next.

