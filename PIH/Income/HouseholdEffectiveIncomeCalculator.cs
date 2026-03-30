using System;

namespace PIH.Income
{
    public static class HouseholdEffectiveIncomeCalculator
    {
        private const double kDaysPerYear = 365d;

        public static EffectiveIncomeBreakdown Calculate(
            HouseholdIncomeInputs inputs,
            float annualReturnPercent,
            int wealthIncomeCapPerDay)
        {
            int wealthIncome = EstimateWealthIncome(
                inputs.LiquidWealth,
                annualReturnPercent,
                wealthIncomeCapPerDay);

            return new EffectiveIncomeBreakdown(
                inputs.LaborIncome,
                inputs.TransferIncome,
                inputs.EmployeeProfitShare,
                wealthIncome,
                inputs.SmoothedIncomeAdjustment);
        }

        public static int EstimateWealthIncome(
            int liquidWealth,
            float annualReturnPercent,
            int wealthIncomeCapPerDay)
        {
            if (liquidWealth <= 0 || annualReturnPercent <= 0f || wealthIncomeCapPerDay <= 0)
            {
                return 0;
            }

            double dailyReturnRate = annualReturnPercent / 100d / kDaysPerYear;
            int unclampedValue = (int)Math.Round(liquidWealth * dailyReturnRate, MidpointRounding.AwayFromZero);
            int nonNegativeValue = Math.Max(0, unclampedValue);
            return Math.Min(nonNegativeValue, wealthIncomeCapPerDay);
        }
    }
}
