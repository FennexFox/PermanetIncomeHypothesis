using System.Collections.Generic;

namespace PIH.Patches
{
    public static class PatchPlanRegistry
    {
        private static readonly HousingAffordabilityPatchTarget[] s_Targets =
        {
            new HousingAffordabilityPatchTarget(
                "HouseholdBehaviorSystem",
                "No-money and distress checks",
                "Short-run resource checks can trigger unrealistic move-outs for wealthy households with low current flow income.",
                optional: false),
            new HousingAffordabilityPatchTarget(
                "CitizenPathfindSetup",
                "Candidate-home affordability filter",
                "This is where homes are filtered out before scoring, so an improved income signal must land here early.",
                optional: false),
            new HousingAffordabilityPatchTarget(
                "HouseholdFindPropertySystem",
                "Final rental selection gate",
                "The final home-selection pass repeats affordability checks and can override earlier pathfinding options.",
                optional: false),
            new HousingAffordabilityPatchTarget(
                "RentAdjustSystem",
                "High-rent pressure and property-seeker activation",
                "This call site currently blends income with cash-on-hand and directly affects high-rent warnings.",
                optional: false),
            new HousingAffordabilityPatchTarget(
                "SicknessCheckSystem",
                "Secondary affordability-dependent follow-up",
                "Keep this as a later validation target if health-side move or welfare behavior still assumes vanilla income.",
                optional: true)
        };

        public static IReadOnlyList<HousingAffordabilityPatchTarget> Targets => s_Targets;
    }
}

