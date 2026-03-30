namespace PIH.Income
{
    public readonly struct EffectiveIncomeBreakdown
    {
        public EffectiveIncomeBreakdown(
            int laborIncome,
            int transferIncome,
            int employeeProfitShare,
            int wealthIncome,
            int smoothedIncomeAdjustment)
        {
            LaborIncome = laborIncome;
            TransferIncome = transferIncome;
            EmployeeProfitShare = employeeProfitShare;
            WealthIncome = wealthIncome;
            SmoothedIncomeAdjustment = smoothedIncomeAdjustment;
        }

        public int LaborIncome { get; }
        public int TransferIncome { get; }
        public int EmployeeProfitShare { get; }
        public int WealthIncome { get; }
        public int SmoothedIncomeAdjustment { get; }
        public int Total => LaborIncome + TransferIncome + EmployeeProfitShare + WealthIncome + SmoothedIncomeAdjustment;
    }
}
