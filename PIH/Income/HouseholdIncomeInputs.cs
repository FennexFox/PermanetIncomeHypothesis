namespace PIH.Income
{
    public readonly struct HouseholdIncomeInputs
    {
        public HouseholdIncomeInputs(
            int laborIncome,
            int transferIncome,
            int employeeProfitShare,
            int liquidWealth,
            int smoothedIncomeAdjustment)
        {
            LaborIncome = laborIncome;
            TransferIncome = transferIncome;
            EmployeeProfitShare = employeeProfitShare;
            LiquidWealth = liquidWealth;
            SmoothedIncomeAdjustment = smoothedIncomeAdjustment;
        }

        public int LaborIncome { get; }
        public int TransferIncome { get; }
        public int EmployeeProfitShare { get; }
        public int LiquidWealth { get; }
        public int SmoothedIncomeAdjustment { get; }
    }
}
