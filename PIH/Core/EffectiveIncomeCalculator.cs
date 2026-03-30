using System;
using PIH.Models;

namespace PIH.Core
{
    public static class EffectiveIncomeCalculator
    {
        public static EffectiveIncomeBreakdown Calculate(EffectiveIncomeInputs inputs)
        {
            float wealthIncome = CalculateWealthIncome(
                inputs.LiquidWealth,
                inputs.WealthReturnRatePerDay,
                inputs.DailyWealthIncomeCap);

            float protectedLiquidityTarget = Math.Max(0f, inputs.BaseFlowIncome) * Math.Max(0, inputs.LiquidWealthBufferDays);
            float protectedLiquidity = Clamp(inputs.LiquidWealth, 0f, protectedLiquidityTarget);
            float spendableLiquidity = Math.Max(0f, inputs.LiquidWealth - protectedLiquidity);
            float effectiveIncome = inputs.BaseFlowIncome + wealthIncome + inputs.SmoothedIncomeAdjustment;

            return new EffectiveIncomeBreakdown(
                inputs.LaborIncome,
                inputs.TransferIncome,
                inputs.EmployeeProfitShare,
                wealthIncome,
                inputs.SmoothedIncomeAdjustment,
                protectedLiquidity,
                spendableLiquidity,
                effectiveIncome);
        }

        public static float CalculateWealthIncome(float liquidWealth, float returnRatePerDay, float dailyCap)
        {
            float baseIncome = Math.Max(0f, liquidWealth) * Math.Max(0f, returnRatePerDay);
            return Clamp(baseIncome, 0f, Math.Max(0f, dailyCap));
        }

        private static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }
    }
}

