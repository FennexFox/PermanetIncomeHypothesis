using System;
using PIH.Constants;
using PIH.Models;

namespace PIH.Core
{
    public static class EffectiveIncomeCalculator
    {
        public static EffectiveIncomeBreakdown Calculate(
            EffectiveIncomeInputs inputs,
            float structuralAnchorAdditiveFloor = PihBalanceDefaults.StructuralAnchorAdditiveFloor,
            float structuralAnchorRelativeMarginShare = PihBalanceDefaults.StructuralAnchorRelativeMarginPercent / 100f,
            float alignedMinBurden = ProxySignalEstimator.DefaultAlignedMinBurdenRatio,
            float alignedMaxBurden = ProxySignalEstimator.DefaultAlignedMaxBurdenRatio)
        {
            float currentIncome = Math.Max(0f, inputs.CurrentIncome);
            float smoothedObservedIncome = Math.Max(0f, inputs.SmoothedObservedIncome);
            float observedBaseIncome = Math.Max(currentIncome, smoothedObservedIncome);

            float workerContribution = Math.Max(0f, inputs.WorkerContribution);
            float nonWorkerPotentialContribution = Math.Max(0f, inputs.NonWorkerPotentialContribution);
            float studentPotentialContribution = Math.Max(0f, inputs.StudentPotentialContribution);
            float guaranteedTransferAnchor = Math.Max(0f, inputs.GuaranteedTransferAnchor);
            float earningsAnchorRaw = workerContribution + nonWorkerPotentialContribution + studentPotentialContribution;
            float structuralAnchorRaw = guaranteedTransferAnchor + earningsAnchorRaw;

            float attachmentScore = Clamp(inputs.AttachmentScore, 0f, 1f);
            float additiveFloor = Math.Max(0f, structuralAnchorAdditiveFloor);
            float relativeMarginShare = Math.Max(0f, structuralAnchorRelativeMarginShare);
            float anchorBase = Math.Max(observedBaseIncome, guaranteedTransferAnchor);
            float earningsAnchorCap =
                Math.Max(0f, anchorBase - guaranteedTransferAnchor) +
                additiveFloor +
                (observedBaseIncome * relativeMarginShare * attachmentScore);
            float cappedEarningsAnchor = Clamp(earningsAnchorRaw, 0f, earningsAnchorCap);
            float cappedStructuralAnchor = guaranteedTransferAnchor + cappedEarningsAnchor;

            float vehicleSignal = Math.Max(0f, inputs.VehicleSignal);
            float liquidWealthSignal = Math.Max(0f, inputs.LiquidWealthSignal);
            float effectiveIncome = Math.Max(observedBaseIncome, cappedStructuralAnchor) + vehicleSignal + liquidWealthSignal;
            float housingCostPerDay = inputs.HousingCostPerDay;
            float housingBurdenRatio = ProxySignalEstimator.CalculateHousingBurdenRatio(housingCostPerDay, effectiveIncome);
            HousingConsistencyState housingConsistencyState = ProxySignalEstimator.ClassifyHousingConsistency(
                housingCostPerDay,
                effectiveIncome,
                alignedMinBurden,
                alignedMaxBurden);

            return new EffectiveIncomeBreakdown(
                currentIncome,
                smoothedObservedIncome,
                observedBaseIncome,
                workerContribution,
                nonWorkerPotentialContribution,
                studentPotentialContribution,
                guaranteedTransferAnchor,
                earningsAnchorRaw,
                structuralAnchorRaw,
                attachmentScore,
                earningsAnchorCap,
                cappedEarningsAnchor,
                cappedStructuralAnchor,
                vehicleSignal,
                liquidWealthSignal,
                housingCostPerDay,
                housingBurdenRatio,
                housingConsistencyState,
                effectiveIncome);
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
