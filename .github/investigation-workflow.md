# Investigation Workflow

1. Reproduce the symptom with the smallest useful scenario.
2. Identify the exact vanilla decision point involved.
3. Record why the current behavior appears incorrect or incomplete.
4. Decide whether the next step is instrumentation, a narrow Harmony patch, a
   system-side follow-up pass, or design clarification only.
5. Land the smallest change that produces new evidence or a bounded fix.
6. Update docs so future work starts from the new state instead of rediscovering
   it.