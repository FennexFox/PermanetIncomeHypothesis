using System.Collections.Generic;

namespace PIH.Patches
{
    public static class HouseholdAffordabilityPatchPlan
    {
        public static IReadOnlyList<PatchTargetDescriptor> Targets { get; } =
            new[]
            {
                new PatchTargetDescriptor(
                    "HouseholdBehaviorSystem",
                    "Reduce false NoMoney move-outs by replacing short-run resource checks with effective-income evidence.",
                    "System patch or helper substitution",
                    "P0"),
                new PatchTargetDescriptor(
                    "CitizenPathfindSetup",
                    "Swap rental candidate filtering away from wage-like income only.",
                    "Helper patch in Burst-sensitive pathfinding code",
                    "P0"),
                new PatchTargetDescriptor(
                    "HouseholdFindPropertySystem",
                    "Apply PIH-based affordability checks during final home selection.",
                    "System patch",
                    "P0"),
                new PatchTargetDescriptor(
                    "RentAdjustSystem",
                    "Replace high-rent and PropertySeeker triggers that currently key off income plus positive cash on hand.",
                    "System patch",
                    "P0"),
                new PatchTargetDescriptor(
                    "PropertyRenterSystem",
                    "Keep household money roughly aligned with the PIH model without making the mod a cash faucet.",
                    "Late accounting support system",
                    "P1"),
                new PatchTargetDescriptor(
                    "SicknessCheckSystem",
                    "Optional follow-up if sickness or care behavior also depends on narrow income gates.",
                    "Optional helper patch",
                    "P2"),
            };
    }
}
