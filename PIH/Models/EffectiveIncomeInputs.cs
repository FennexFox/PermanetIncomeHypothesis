namespace PIH.Models
{
    public struct EffectiveIncomeInputs
    {
        public EffectiveIncomeInputs(
            float currentIncome,
            float smoothedObservedIncome,
            float workerContribution,
            float nonWorkerPotentialContribution,
            float studentPotentialContribution,
            float guaranteedTransferAnchor,
            float attachmentScore,
            float housingCostPerDay,
            float vehicleSignal,
            float liquidWealthSignal)
        {
            CurrentIncome = currentIncome;
            SmoothedObservedIncome = smoothedObservedIncome;
            WorkerContribution = workerContribution;
            NonWorkerPotentialContribution = nonWorkerPotentialContribution;
            StudentPotentialContribution = studentPotentialContribution;
            GuaranteedTransferAnchor = guaranteedTransferAnchor;
            AttachmentScore = attachmentScore;
            HousingCostPerDay = housingCostPerDay;
            VehicleSignal = vehicleSignal;
            LiquidWealthSignal = liquidWealthSignal;
        }

        public float CurrentIncome { get; }

        public float SmoothedObservedIncome { get; }

        public float WorkerContribution { get; }

        public float NonWorkerPotentialContribution { get; }

        public float StudentPotentialContribution { get; }

        public float GuaranteedTransferAnchor { get; }

        public float AttachmentScore { get; }

        public float HousingCostPerDay { get; }

        public float VehicleSignal { get; }

        public float LiquidWealthSignal { get; }
    }
}
