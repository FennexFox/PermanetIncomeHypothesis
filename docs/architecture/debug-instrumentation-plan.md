# Debug Instrumentation Plan

## Goal

Make it easy to inspect how the new affordability model differs from vanilla at
the moment a household keeps, finds, or loses housing.

## First Diagnostics To Add

- current income
- smoothed observed income
- guaranteed transfer anchor
- worker contribution
- non-worker potential contribution
- student potential contribution
- earnings anchor raw
- attachment score
- earnings anchor cap
- capped structural anchor
- vehicle signal
- liquid wealth signal
- effective income
- asking rent
- housing burden ratio
- housing consistency state
- affordability multiplier in effect

## Current Scaffold Support

- [PIH/Debugging/HouseholdAffordabilitySnapshot.cs](../../PIH/Debugging/HouseholdAffordabilitySnapshot.cs)
- [PIH/Systems/PermanentIncomeDiagnosticsSystem.cs](../../PIH/Systems/PermanentIncomeDiagnosticsSystem.cs)

## Logging Policy

- default to quiet behavior
- gate detailed logs behind a setting
- prefer compact machine-readable fields over prose-heavy traces
- include enough context to compare vanilla and modded decisions on the same
  household
