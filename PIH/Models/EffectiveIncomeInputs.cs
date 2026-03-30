namespace PIH.Models
{
    public struct EffectiveIncomeInputs
    {
        public EffectiveIncomeInputs(
            float laborIncome,
            float transferIncome,
            float employeeProfitShare,
            float liquidWealth,
            float smoothedIncomeAdjustment,
            float wealthReturnRatePerDay,
            float dailyWealthIncomeCap,
            int liquidWealthBufferDays)
        {
            LaborIncome = laborIncome;
            TransferIncome = transferIncome;
            EmployeeProfitShare = employeeProfitShare;
            LiquidWealth = liquidWealth;
            SmoothedIncomeAdjustment = smoothedIncomeAdjustment;
            WealthReturnRatePerDay = wealthReturnRatePerDay;
            DailyWealthIncomeCap = dailyWealthIncomeCap;
            LiquidWealthBufferDays = liquidWealthBufferDays;
        }

        public float LaborIncome { get; }

        public float TransferIncome { get; }

        public float EmployeeProfitShare { get; }

        public float LiquidWealth { get; }

        public float SmoothedIncomeAdjustment { get; }

        public float WealthReturnRatePerDay { get; }

        public float DailyWealthIncomeCap { get; }

        public int LiquidWealthBufferDays { get; }

        public float BaseFlowIncome => LaborIncome + TransferIncome + EmployeeProfitShare;
    }
}

