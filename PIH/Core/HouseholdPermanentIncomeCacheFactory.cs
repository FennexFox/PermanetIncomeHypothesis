using PIH.Constants;
using PIH.Models;

namespace PIH.Core
{
    public static class HouseholdPermanentIncomeCacheFactory
    {
        public static HouseholdPermanentIncomeCache Create(
            EffectiveIncomeInputs inputs,
            int lastUpdatedDay,
            int updatePartition,
            float structuralAnchorAdditiveFloor = PihBalanceDefaults.StructuralAnchorAdditiveFloor,
            float structuralAnchorRelativeMarginShare = PihBalanceDefaults.StructuralAnchorRelativeMarginPercent / 100f,
            float alignedMinBurden = ProxySignalEstimator.DefaultAlignedMinBurdenRatio,
            float alignedMaxBurden = ProxySignalEstimator.DefaultAlignedMaxBurdenRatio)
        {
            EffectiveIncomeBreakdown breakdown = EffectiveIncomeCalculator.Calculate(
                inputs,
                structuralAnchorAdditiveFloor,
                structuralAnchorRelativeMarginShare,
                alignedMinBurden,
                alignedMaxBurden);

            return new HouseholdPermanentIncomeCache(
                breakdown.CurrentIncome,
                breakdown.SmoothedObservedIncome,
                breakdown.WorkerContribution,
                breakdown.NonWorkerPotentialContribution,
                breakdown.StudentPotentialContribution,
                breakdown.GuaranteedTransferAnchor,
                breakdown.EarningsAnchorRaw,
                breakdown.AttachmentScore,
                breakdown.EarningsAnchorCap,
                breakdown.CappedEarningsAnchor,
                breakdown.CappedStructuralAnchor,
                breakdown.VehicleSignal,
                breakdown.LiquidWealthSignal,
                breakdown.HousingCostPerDay,
                breakdown.HousingBurdenRatio,
                breakdown.HousingConsistencyState,
                breakdown.EffectiveIncome,
                lastUpdatedDay,
                updatePartition,
                ResolveConfidence(breakdown));
        }

        private static PermanentIncomeCacheConfidence ResolveConfidence(EffectiveIncomeBreakdown breakdown)
        {
            if (breakdown.ObservedBaseIncome > 0f && breakdown.CappedStructuralAnchor > 0f)
            {
                return PermanentIncomeCacheConfidence.Stable;
            }

            if (breakdown.ObservedBaseIncome > 0f || breakdown.CappedStructuralAnchor > 0f)
            {
                return PermanentIncomeCacheConfidence.Warm;
            }

            return PermanentIncomeCacheConfidence.Cold;
        }
    }
}
