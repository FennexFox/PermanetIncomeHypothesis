using System;
using PIH.Models;

namespace PIH.Core
{
    public static class ProxySignalEstimator
    {
        public const float DefaultHealthDamping = 0.75f;
        public const float DefaultElderlyLifecycleDamping = 0.35f;
        public const float DefaultAdultStudentAttachment = 0.35f;
        public const float DefaultNonWorkerBaseAttachment = 0.60f;
        public const float DefaultTimeAttachmentHorizonDays = 180f;
        public const float DefaultAlignedMinBurdenRatio = 0.15f;
        public const float DefaultAlignedMaxBurdenRatio = 0.45f;

        public static float EstimateWorkerContribution(float netWage)
        {
            return Math.Max(0f, netWage);
        }

        public static float EstimateNonWorkerPotentialContribution(
            float educationNetWage,
            float nonWorkerPotentialShare,
            float healthDamping,
            float lifecycleDamping)
        {
            return Math.Max(0f, educationNetWage) *
                Clamp(nonWorkerPotentialShare, 0f, 1f) *
                Clamp(healthDamping, 0f, 1f) *
                Clamp(lifecycleDamping, 0f, 1f);
        }

        public static float EstimateStudentPotentialContribution(
            float educationNetWage,
            float studentPotentialShare,
            float healthDamping)
        {
            return Math.Max(0f, educationNetWage) *
                Clamp(studentPotentialShare, 0f, 1f) *
                Clamp(healthDamping, 0f, 1f);
        }

        public static float EstimateTransferContribution(
            bool isChildOrTeen,
            bool isElderly,
            float familyAllowance,
            float pension)
        {
            if (isElderly)
            {
                return Math.Max(0f, pension);
            }

            if (isChildOrTeen)
            {
                return Math.Max(0f, familyAllowance);
            }

            return 0f;
        }

        public static float EstimateHealthDamping(bool hasHealthProblem, int sicknessPenalty)
        {
            return hasHealthProblem || sicknessPenalty > 0
                ? DefaultHealthDamping
                : 1f;
        }

        public static float EstimateLifecycleDamping(bool isAdult, bool isElderly)
        {
            if (isElderly)
            {
                return DefaultElderlyLifecycleDamping;
            }

            if (isAdult)
            {
                return 1f;
            }

            return 0f;
        }

        public static float EstimateCounterAttachment(int unemploymentCounter, float unemploymentAllowanceMaxDays, int wageUpdatesPerDay)
        {
            if (unemploymentCounter <= 0)
            {
                return 1f;
            }

            if (unemploymentAllowanceMaxDays <= 0f || wageUpdatesPerDay <= 0)
            {
                return 0f;
            }

            float maxCounter = unemploymentAllowanceMaxDays * wageUpdatesPerDay;
            if (maxCounter <= 0f)
            {
                return 0f;
            }

            return 1f - Clamp(unemploymentCounter / maxCounter, 0f, 1f);
        }

        public static float EstimateTimeAttachment(float unemploymentTimeCounter, float timeAttachmentHorizonDays = DefaultTimeAttachmentHorizonDays)
        {
            if (unemploymentTimeCounter <= 0f)
            {
                return 1f;
            }

            if (timeAttachmentHorizonDays <= 0f)
            {
                return 0f;
            }

            return 1f - Clamp(unemploymentTimeCounter / timeAttachmentHorizonDays, 0f, 1f);
        }

        public static float EstimateAdultAttachment(
            bool isWorker,
            bool isAdultStudent,
            float counterAttachment,
            float timeAttachment,
            float healthDamping,
            float lifecycleDamping)
        {
            if (isWorker)
            {
                return 1f;
            }

            if (isAdultStudent)
            {
                return Clamp(DefaultAdultStudentAttachment * Clamp(healthDamping, 0f, 1f), 0f, 1f);
            }

            return Clamp(
                DefaultNonWorkerBaseAttachment *
                Clamp(counterAttachment, 0f, 1f) *
                Clamp(timeAttachment, 0f, 1f) *
                Clamp(healthDamping, 0f, 1f) *
                Clamp(lifecycleDamping, 0f, 1f),
                0f,
                1f);
        }

        public static float EstimateAttachmentScore(params float[] adultAttachments)
        {
            if (adultAttachments == null || adultAttachments.Length == 0)
            {
                return 0f;
            }

            float total = 0f;
            int count = 0;
            for (int i = 0; i < adultAttachments.Length; i++)
            {
                total += Clamp(adultAttachments[i], 0f, 1f);
                count++;
            }

            return count > 0 ? total / count : 0f;
        }

        public static float EstimateVehicleSignal(int ownedVehicleCount, float perVehicleBonus)
        {
            if (ownedVehicleCount <= 0 || perVehicleBonus <= 0f)
            {
                return 0f;
            }

            return ownedVehicleCount * perVehicleBonus;
        }

        public static float EstimateLiquidWealthSignal(float liquidWealth, float sqrtMultiplier, float cap)
        {
            if (liquidWealth <= 0f || sqrtMultiplier <= 0f || cap <= 0f)
            {
                return 0f;
            }

            float rawValue = (float)Math.Sqrt(liquidWealth) * sqrtMultiplier;
            return Clamp(rawValue, 0f, cap);
        }

        public static float CalculateHousingBurdenRatio(float housingCostPerDay, float effectiveIncome)
        {
            if (housingCostPerDay < 0f)
            {
                return -1f;
            }

            return Math.Max(0f, housingCostPerDay) / Math.Max(1f, effectiveIncome);
        }

        public static HousingConsistencyState ClassifyHousingConsistency(
            float housingCostPerDay,
            float effectiveIncome,
            float alignedMinBurden = DefaultAlignedMinBurdenRatio,
            float alignedMaxBurden = DefaultAlignedMaxBurdenRatio)
        {
            if (housingCostPerDay < 0f)
            {
                return HousingConsistencyState.Unknown;
            }

            float normalizedMin = Math.Max(0f, alignedMinBurden);
            float normalizedMax = Math.Max(normalizedMin, alignedMaxBurden);
            float burdenRatio = CalculateHousingBurdenRatio(housingCostPerDay, effectiveIncome);

            if (burdenRatio < normalizedMin)
            {
                return HousingConsistencyState.Underhoused;
            }

            if (burdenRatio <= normalizedMax)
            {
                return HousingConsistencyState.Aligned;
            }

            return HousingConsistencyState.Overburdened;
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
