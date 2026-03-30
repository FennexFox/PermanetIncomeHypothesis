using PIH.Income;

namespace PIH.Debugging
{
    public readonly struct HouseholdIncomeDebugSnapshot
    {
        public HouseholdIncomeDebugSnapshot(
            string householdId,
            EffectiveIncomeBreakdown incomeBreakdown,
            int liquidWealth,
            int askingRent,
            bool supportEligible,
            string affordabilityTarget)
        {
            HouseholdId = householdId;
            IncomeBreakdown = incomeBreakdown;
            LiquidWealth = liquidWealth;
            AskingRent = askingRent;
            SupportEligible = supportEligible;
            AffordabilityTarget = affordabilityTarget;
        }

        public string HouseholdId { get; }
        public EffectiveIncomeBreakdown IncomeBreakdown { get; }
        public int LiquidWealth { get; }
        public int AskingRent { get; }
        public bool SupportEligible { get; }
        public string AffordabilityTarget { get; }
    }
}
