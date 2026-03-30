namespace PIH.Models
{
    public struct EffectiveIncomeBreakdown
    {
        public EffectiveIncomeBreakdown(
            float laborIncome,
            float transferIncome,
            float employeeProfitShare,
            float wealthIncome,
            float smoothedIncomeAdjustment,
            float protectedLiquidity,
            float spendableLiquidity,
            float effectiveIncome)
        {
            LaborIncome = laborIncome;
            TransferIncome = transferIncome;
            EmployeeProfitShare = employeeProfitShare;
            WealthIncome = wealthIncome;
            SmoothedIncomeAdjustment = smoothedIncomeAdjustment;
            ProtectedLiquidity = protectedLiquidity;
            SpendableLiquidity = spendableLiquidity;
            EffectiveIncome = effectiveIncome;
        }

        public float LaborIncome { get; }

        public float TransferIncome { get; }

        public float EmployeeProfitShare { get; }

        public float WealthIncome { get; }

        public float SmoothedIncomeAdjustment { get; }

        public float ProtectedLiquidity { get; }

        public float SpendableLiquidity { get; }

        public float EffectiveIncome { get; }
    }
}

