namespace PIH.Models
{
    public struct HouseholdPermanentIncomeCache
    {
        public HouseholdPermanentIncomeCache(
            float currentIncome,
            float smoothedObservedIncome,
            float workerContribution,
            float nonWorkerPotentialContribution,
            float studentPotentialContribution,
            float guaranteedTransferAnchor,
            float earningsAnchorRaw,
            float attachmentScore,
            float earningsAnchorCap,
            float cappedEarningsAnchor,
            float cappedStructuralAnchor,
            float vehicleSignal,
            float liquidWealthSignal,
            float housingCostPerDay,
            float housingBurdenRatio,
            HousingConsistencyState housingConsistencyState,
            float effectiveIncome,
            int lastUpdatedDay,
            int updatePartition,
            PermanentIncomeCacheConfidence confidence)
        {
            CurrentIncome = currentIncome;
            SmoothedObservedIncome = smoothedObservedIncome;
            WorkerContribution = workerContribution;
            NonWorkerPotentialContribution = nonWorkerPotentialContribution;
            StudentPotentialContribution = studentPotentialContribution;
            GuaranteedTransferAnchor = guaranteedTransferAnchor;
            EarningsAnchorRaw = earningsAnchorRaw;
            AttachmentScore = attachmentScore;
            EarningsAnchorCap = earningsAnchorCap;
            CappedEarningsAnchor = cappedEarningsAnchor;
            CappedStructuralAnchor = cappedStructuralAnchor;
            VehicleSignal = vehicleSignal;
            LiquidWealthSignal = liquidWealthSignal;
            HousingCostPerDay = housingCostPerDay;
            HousingBurdenRatio = housingBurdenRatio;
            HousingConsistencyState = housingConsistencyState;
            EffectiveIncome = effectiveIncome;
            LastUpdatedDay = lastUpdatedDay;
            UpdatePartition = updatePartition;
            Confidence = confidence;
        }

        public float CurrentIncome { get; }

        public float SmoothedObservedIncome { get; }

        public float WorkerContribution { get; }

        public float NonWorkerPotentialContribution { get; }

        public float StudentPotentialContribution { get; }

        public float GuaranteedTransferAnchor { get; }

        public float EarningsAnchorRaw { get; }

        public float AttachmentScore { get; }

        public float EarningsAnchorCap { get; }

        public float CappedEarningsAnchor { get; }

        public float CappedStructuralAnchor { get; }

        public float VehicleSignal { get; }

        public float LiquidWealthSignal { get; }

        public float HousingCostPerDay { get; }

        public float HousingBurdenRatio { get; }

        public HousingConsistencyState HousingConsistencyState { get; }

        public float EffectiveIncome { get; }

        public int LastUpdatedDay { get; }

        public int UpdatePartition { get; }

        public PermanentIncomeCacheConfidence Confidence { get; }
    }
}
