using Game;
using PIH.Core;
using PIH.Models;
using UnityEngine.Scripting;

namespace PIH.Systems
{
    [Preserve]
    public partial class PermanentIncomeRuntimeSystem : GameSystemBase
    {
        private int m_SampleCountdown;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_SampleCountdown = 0;
        }

        protected override void OnUpdate()
        {
            if (Mod.Settings == null || !Mod.Settings.EnablePermanentIncomeLayer)
            {
                return;
            }

            if (!Mod.Settings.EnableDiagnostics)
            {
                return;
            }

            if (m_SampleCountdown > 0)
            {
                m_SampleCountdown--;
                return;
            }

            m_SampleCountdown = GetSampleStride(Mod.Settings.DiagnosticsSamplesPerDay);

            EffectiveIncomeInputs inputs = new EffectiveIncomeInputs(
                laborIncome: 250f,
                transferIncome: 40f,
                employeeProfitShare: 10f,
                liquidWealth: 12000f,
                smoothedIncomeAdjustment: Mod.Settings.EnableIncomeSmoothing ? 15f : 0f,
                wealthReturnRatePerDay: Mod.Settings.EnableWealthDerivedIncome ? Mod.Settings.GetWealthReturnRatePerDay() : 0f,
                dailyWealthIncomeCap: Mod.Settings.DailyWealthIncomeCap,
                liquidWealthBufferDays: Mod.Settings.LiquidWealthBufferDays);

            EffectiveIncomeBreakdown breakdown = EffectiveIncomeCalculator.Calculate(inputs);
            Mod.log.Info(
                $"PIH scaffold sample -> effectiveIncome={breakdown.EffectiveIncome:0.##}, wealthIncome={breakdown.WealthIncome:0.##}, protectedLiquidity={breakdown.ProtectedLiquidity:0.##}, spendableLiquidity={breakdown.SpendableLiquidity:0.##}.");
        }

        private static int GetSampleStride(int samplesPerDay)
        {
            if (samplesPerDay <= 1)
            {
                return 2048;
            }

            int clamped = samplesPerDay < 1 ? 1 : (samplesPerDay > 8 ? 8 : samplesPerDay);
            return 2048 / clamped;
        }
    }
}

