using PIH.Models;

namespace PIH.Debugging
{
    public struct HouseholdAffordabilitySnapshot
    {
        public HouseholdAffordabilitySnapshot(
            float askingRent,
            float affordabilityMultiplier,
            EffectiveIncomeBreakdown incomeBreakdown)
        {
            AskingRent = askingRent;
            AffordabilityMultiplier = affordabilityMultiplier;
            IncomeBreakdown = incomeBreakdown;
            AffordableRentCeiling = incomeBreakdown.EffectiveIncome * affordabilityMultiplier;
            RentToIncomeRatio = incomeBreakdown.EffectiveIncome > 0f
                ? askingRent / incomeBreakdown.EffectiveIncome
                : float.PositiveInfinity;
        }

        public float AskingRent { get; }

        public float AffordabilityMultiplier { get; }

        public EffectiveIncomeBreakdown IncomeBreakdown { get; }

        public float AffordableRentCeiling { get; }

        public float RentToIncomeRatio { get; }

        public bool CanAfford => AskingRent <= AffordableRentCeiling;
    }
}
