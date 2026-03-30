using PIH.Core;
using PIH.Models;
using Xunit;

namespace PIH.Tests;

public class EffectiveIncomeCalculatorTests
{
    [Fact]
    public void Calculate_PreservesTransferAndCapsEarningsWhenObservedIncomeIsZero()
    {
        var inputs = new EffectiveIncomeInputs(
            currentIncome: 0f,
            smoothedObservedIncome: 0f,
            workerContribution: 0f,
            nonWorkerPotentialContribution: 100f,
            studentPotentialContribution: 10f,
            guaranteedTransferAnchor: 50f,
            attachmentScore: 0.2f,
            housingCostPerDay: 30f,
            vehicleSignal: 0f,
            liquidWealthSignal: 0f);

        var breakdown = EffectiveIncomeCalculator.Calculate(inputs);

        Assert.Equal(0f, breakdown.ObservedBaseIncome);
        Assert.Equal(50f, breakdown.GuaranteedTransferAnchor);
        Assert.Equal(110f, breakdown.EarningsAnchorRaw);
        Assert.Equal(25f, breakdown.EarningsAnchorCap);
        Assert.Equal(25f, breakdown.CappedEarningsAnchor);
        Assert.Equal(75f, breakdown.CappedStructuralAnchor);
        Assert.Equal(75f, breakdown.EffectiveIncome);
    }

    [Fact]
    public void Calculate_ComputesExpectedCapAboveObservedBase()
    {
        var inputs = new EffectiveIncomeInputs(
            currentIncome: 200f,
            smoothedObservedIncome: 180f,
            workerContribution: 120f,
            nonWorkerPotentialContribution: 180f,
            studentPotentialContribution: 20f,
            guaranteedTransferAnchor: 20f,
            attachmentScore: 0.5f,
            housingCostPerDay: 80f,
            vehicleSignal: 10f,
            liquidWealthSignal: 5f);

        var breakdown = EffectiveIncomeCalculator.Calculate(inputs);

        Assert.Equal(200f, breakdown.ObservedBaseIncome);
        Assert.Equal(320f, breakdown.EarningsAnchorRaw);
        Assert.Equal(235f, breakdown.EarningsAnchorCap);
        Assert.Equal(235f, breakdown.CappedEarningsAnchor);
        Assert.Equal(255f, breakdown.CappedStructuralAnchor);
        Assert.Equal(270f, breakdown.EffectiveIncome);
    }

    [Fact]
    public void Calculate_HousingCostDoesNotChangeEffectiveIncome()
    {
        var baseInputs = new EffectiveIncomeInputs(
            currentIncome: 180f,
            smoothedObservedIncome: 150f,
            workerContribution: 80f,
            nonWorkerPotentialContribution: 70f,
            studentPotentialContribution: 0f,
            guaranteedTransferAnchor: 10f,
            attachmentScore: 0.4f,
            housingCostPerDay: 20f,
            vehicleSignal: 15f,
            liquidWealthSignal: 5f);

        var lowerHousing = EffectiveIncomeCalculator.Calculate(baseInputs);
        var higherHousing = EffectiveIncomeCalculator.Calculate(new EffectiveIncomeInputs(
            baseInputs.CurrentIncome,
            baseInputs.SmoothedObservedIncome,
            baseInputs.WorkerContribution,
            baseInputs.NonWorkerPotentialContribution,
            baseInputs.StudentPotentialContribution,
            baseInputs.GuaranteedTransferAnchor,
            baseInputs.AttachmentScore,
            housingCostPerDay: 120f,
            baseInputs.VehicleSignal,
            baseInputs.LiquidWealthSignal));

        Assert.Equal(lowerHousing.EffectiveIncome, higherHousing.EffectiveIncome);
        Assert.NotEqual(lowerHousing.HousingBurdenRatio, higherHousing.HousingBurdenRatio);
    }

    [Theory]
    [InlineData(-1f, 100f, HousingConsistencyState.Unknown)]
    [InlineData(10f, 100f, HousingConsistencyState.Underhoused)]
    [InlineData(20f, 100f, HousingConsistencyState.Aligned)]
    [InlineData(60f, 100f, HousingConsistencyState.Overburdened)]
    public void ClassifyHousingConsistency_UsesExpectedThresholds(float housingCost, float effectiveIncome, HousingConsistencyState expected)
    {
        Assert.Equal(expected, ProxySignalEstimator.ClassifyHousingConsistency(housingCost, effectiveIncome));
    }

    [Fact]
    public void HouseholdPermanentIncomeCacheFactory_SetsConfidenceFromObservedAndStructuralSignals()
    {
        var cold = HouseholdPermanentIncomeCacheFactory.Create(
            new EffectiveIncomeInputs(0f, 0f, 0f, 0f, 0f, 0f, 0f, -1f, 0f, 0f),
            1,
            0);
        var warm = HouseholdPermanentIncomeCacheFactory.Create(
            new EffectiveIncomeInputs(0f, 0f, 0f, 0f, 0f, 10f, 0f, -1f, 0f, 0f),
            1,
            0);
        var stable = HouseholdPermanentIncomeCacheFactory.Create(
            new EffectiveIncomeInputs(50f, 40f, 0f, 0f, 0f, 10f, 0f, -1f, 0f, 0f),
            1,
            0);

        Assert.Equal(PermanentIncomeCacheConfidence.Cold, cold.Confidence);
        Assert.Equal(PermanentIncomeCacheConfidence.Warm, warm.Confidence);
        Assert.Equal(PermanentIncomeCacheConfidence.Stable, stable.Confidence);
    }
}
