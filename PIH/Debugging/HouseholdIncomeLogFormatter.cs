using System.Globalization;

namespace PIH.Debugging
{
    public static class HouseholdIncomeLogFormatter
    {
        public static string Format(HouseholdIncomeDebugSnapshot snapshot)
        {
            decimal rentBurden = snapshot.IncomeBreakdown.Total > 0
                ? (decimal)snapshot.AskingRent / snapshot.IncomeBreakdown.Total
                : decimal.Zero;

            return string.Format(
                CultureInfo.InvariantCulture,
                "household={0}; target={1}; labor={2}; transfers={3}; profitShare={4}; wealthIncome={5}; smoothedAdjustment={6}; effectiveIncome={7}; liquidWealth={8}; askingRent={9}; supportEligible={10}; rentBurden={11:0.00}",
                snapshot.HouseholdId,
                snapshot.AffordabilityTarget,
                snapshot.IncomeBreakdown.LaborIncome,
                snapshot.IncomeBreakdown.TransferIncome,
                snapshot.IncomeBreakdown.EmployeeProfitShare,
                snapshot.IncomeBreakdown.WealthIncome,
                snapshot.IncomeBreakdown.SmoothedIncomeAdjustment,
                snapshot.IncomeBreakdown.Total,
                snapshot.LiquidWealth,
                snapshot.AskingRent,
                snapshot.SupportEligible,
                rentBurden);
        }
    }
}
