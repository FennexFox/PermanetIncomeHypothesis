namespace PIH.Constants
{
    public static class PihBalanceDefaults
    {
        public const int AffordabilityMultiplierPercent = 100;
        public const int NonWorkerPotentialIncomeSharePercent = 40;
        public const int StudentPotentialIncomeSharePercent = 20;
        public const int StructuralAnchorAdditiveFloor = 25;
        public const int StructuralAnchorRelativeMarginPercent = 30;
        public const int IncomeSmoothingDays = 30;
        public const int VehicleSignalPerVehicle = 25;
        public const float LiquidWealthSignalSqrtMultiplier = 0.2f;
        public const int LiquidWealthSignalCap = 150;
        public const int CacheUpdatePartitions = 32;
        public const int DiagnosticsSamplesPerDay = 2;
    }
}
