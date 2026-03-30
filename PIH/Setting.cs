using System;
using System.Collections.Generic;
using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using PIH.Constants;

namespace PIH
{
    [FileLocation(nameof(PIH))]
    [SettingsUIGroupOrder(kGeneralGroup, kWealthGroup, kSmoothingGroup, kDiagnosticsGroup)]
    [SettingsUIShowGroupName(kGeneralGroup, kWealthGroup, kSmoothingGroup, kDiagnosticsGroup)]
    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kGeneralGroup = "General";
        public const string kWealthGroup = "WealthIncome";
        public const string kSmoothingGroup = "Smoothing";
        public const string kDiagnosticsGroup = "Diagnostics";

        public Setting(IMod mod) : base(mod)
        {
        }

        [SettingsUISection(kSection, kGeneralGroup)]
        public bool EnablePermanentIncomeLayer { get; set; } = true;

        [SettingsUISection(kSection, kGeneralGroup)]
        public bool EnableWealthDerivedIncome { get; set; } = true;

        [SettingsUISection(kSection, kGeneralGroup)]
        public bool EnableIncomeSmoothing { get; set; }

        [SettingsUISection(kSection, kGeneralGroup)]
        [SettingsUISlider(min = 50, max = 200, step = 5, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int AffordabilityMultiplierPercent { get; set; } = PihBalanceDefaults.AffordabilityMultiplierPercent;

        [SettingsUISection(kSection, kWealthGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreWealthSettingsDisabled))]
        [SettingsUISlider(min = 0f, max = 10f, step = 0.25f, scalarMultiplier = 1, unit = Unit.kFloatTwoFractions)]
        public float AnnualWealthReturnPercent { get; set; } = PihBalanceDefaults.AnnualWealthReturnPercent;

        [SettingsUISection(kSection, kWealthGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreWealthSettingsDisabled))]
        [SettingsUISlider(min = 0, max = 10000, step = 50, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int DailyWealthIncomeCap { get; set; } = PihBalanceDefaults.DailyWealthIncomeCap;

        [SettingsUISection(kSection, kWealthGroup)]
        [SettingsUISlider(min = 0, max = 120, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int LiquidWealthBufferDays { get; set; } = PihBalanceDefaults.LiquidWealthBufferDays;

        [SettingsUISection(kSection, kSmoothingGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreSmoothingSettingsDisabled))]
        [SettingsUISlider(min = 1, max = 60, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int IncomeSmoothingDays { get; set; } = PihBalanceDefaults.IncomeSmoothingDays;

        [SettingsUISection(kSection, kDiagnosticsGroup)]
        public bool EnableDiagnostics { get; set; }

        [SettingsUISection(kSection, kDiagnosticsGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreDiagnosticsSettingsDisabled))]
        [SettingsUISlider(min = 1, max = 8, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int DiagnosticsSamplesPerDay { get; set; } = PihBalanceDefaults.DiagnosticsSamplesPerDay;

        public override void SetDefaults()
        {
            EnablePermanentIncomeLayer = true;
            EnableWealthDerivedIncome = true;
            EnableIncomeSmoothing = false;
            AffordabilityMultiplierPercent = PihBalanceDefaults.AffordabilityMultiplierPercent;
            AnnualWealthReturnPercent = PihBalanceDefaults.AnnualWealthReturnPercent;
            DailyWealthIncomeCap = PihBalanceDefaults.DailyWealthIncomeCap;
            LiquidWealthBufferDays = PihBalanceDefaults.LiquidWealthBufferDays;
            IncomeSmoothingDays = PihBalanceDefaults.IncomeSmoothingDays;
            EnableDiagnostics = false;
            DiagnosticsSamplesPerDay = PihBalanceDefaults.DiagnosticsSamplesPerDay;
        }

        public float GetAffordabilityMultiplier()
        {
            return Math.Max(0.5f, AffordabilityMultiplierPercent / 100f);
        }

        public float GetWealthReturnRatePerDay()
        {
            return Math.Max(0f, AnnualWealthReturnPercent) / 100f / PihBalanceDefaults.DaysPerYear;
        }

        private bool AreWealthSettingsDisabled() => !EnableWealthDerivedIncome;

        private bool AreSmoothingSettingsDisabled() => !EnableIncomeSmoothing;

        private bool AreDiagnosticsSettingsDisabled() => !EnableDiagnostics;
    }

    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Permanent Income Hypothesis" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Main" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kGeneralGroup), "General" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kWealthGroup), "Wealth Income" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kSmoothingGroup), "Smoothing" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kDiagnosticsGroup), "Diagnostics" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnablePermanentIncomeLayer)), "Enable permanent income layer" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnablePermanentIncomeLayer)), "Master switch for the mod's effective-income affordability layer. Keep this on when testing PIH behavior, and turn it off when you need a clean vanilla comparison." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableWealthDerivedIncome)), "Enable wealth-derived income" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableWealthDerivedIncome)), "Allows liquid wealth to contribute a capped daily supplement to effective household income." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableIncomeSmoothing)), "Enable income smoothing" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableIncomeSmoothing)), "Reserves a separate tuning path for trailing-income smoothing. The scaffold wires the setting now so the later patch set can adopt it without another settings migration." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AffordabilityMultiplierPercent)), "Affordability multiplier (%)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AffordabilityMultiplierPercent)), "Baseline multiplier applied when converting effective income into an affordability ceiling for housing checks." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AnnualWealthReturnPercent)), "Annual wealth return (%)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AnnualWealthReturnPercent)), "Conservative annualized return used for the liquid-wealth income proxy." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DailyWealthIncomeCap)), "Daily wealth income cap" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DailyWealthIncomeCap)), "Hard cap on the daily wealth-income supplement so liquid wealth does not dominate wages and transfers." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LiquidWealthBufferDays)), "Protected liquidity buffer (days)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LiquidWealthBufferDays)), "How many days of baseline flow income should remain protected before liquid wealth becomes spendable in diagnostics and affordability reasoning." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.IncomeSmoothingDays)), "Income smoothing window (days)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.IncomeSmoothingDays)), "Reserved trailing window for the future smoothing layer." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDiagnostics)), "Enable diagnostics" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDiagnostics)), "Emits scaffold diagnostics so the calculator, patch plan, and future runtime hooks are visible while development is still in progress." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DiagnosticsSamplesPerDay)), "Diagnostics samples per day" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DiagnosticsSamplesPerDay)), "Controls how often the scaffold runtime emits summary diagnostics while the detailed instrumentation layer is being built out." },
            };
        }

        public void Unload()
        {
        }
    }
}
