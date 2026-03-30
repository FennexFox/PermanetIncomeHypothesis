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
    [SettingsUIGroupOrder(kGeneralGroup, kStructuralAnchorGroup, kSmoothingGroup, kSignalsGroup, kCacheGroup, kDiagnosticsGroup)]
    [SettingsUIShowGroupName(kGeneralGroup, kStructuralAnchorGroup, kSmoothingGroup, kSignalsGroup, kCacheGroup, kDiagnosticsGroup)]
    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kGeneralGroup = "General";
        public const string kStructuralAnchorGroup = "StructuralAnchor";
        public const string kSmoothingGroup = "Smoothing";
        public const string kSignalsGroup = "Signals";
        public const string kCacheGroup = "Cache";
        public const string kDiagnosticsGroup = "Diagnostics";

        public Setting(IMod mod) : base(mod)
        {
        }

        [SettingsUISection(kSection, kGeneralGroup)]
        public bool EnablePermanentIncomeLayer { get; set; } = true;

        [SettingsUISection(kSection, kGeneralGroup)]
        [SettingsUISlider(min = 50, max = 200, step = 5, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int AffordabilityMultiplierPercent { get; set; } = PihBalanceDefaults.AffordabilityMultiplierPercent;

        [SettingsUISection(kSection, kStructuralAnchorGroup)]
        [SettingsUISlider(min = 0, max = 100, step = 5, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int NonWorkerPotentialIncomeSharePercent { get; set; } = PihBalanceDefaults.NonWorkerPotentialIncomeSharePercent;

        [SettingsUISection(kSection, kStructuralAnchorGroup)]
        [SettingsUISlider(min = 0, max = 100, step = 5, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int StudentPotentialIncomeSharePercent { get; set; } = PihBalanceDefaults.StudentPotentialIncomeSharePercent;

        [SettingsUISection(kSection, kStructuralAnchorGroup)]
        [SettingsUISlider(min = 0, max = 250, step = 5, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int StructuralAnchorAdditiveFloor { get; set; } = PihBalanceDefaults.StructuralAnchorAdditiveFloor;

        [SettingsUISection(kSection, kStructuralAnchorGroup)]
        [SettingsUISlider(min = 0, max = 100, step = 5, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int StructuralAnchorRelativeMarginPercent { get; set; } = PihBalanceDefaults.StructuralAnchorRelativeMarginPercent;

        [SettingsUISection(kSection, kSmoothingGroup)]
        public bool EnableIncomeSmoothing { get; set; } = true;

        [SettingsUISection(kSection, kSmoothingGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreSmoothingSettingsDisabled))]
        [SettingsUISlider(min = 1, max = 60, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int IncomeSmoothingDays { get; set; } = PihBalanceDefaults.IncomeSmoothingDays;

        [SettingsUISection(kSection, kSignalsGroup)]
        public bool EnableVehicleSignal { get; set; } = true;

        [SettingsUISection(kSection, kSignalsGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreVehicleSignalSettingsDisabled))]
        [SettingsUISlider(min = 0, max = 250, step = 5, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int VehicleSignalPerVehicle { get; set; } = PihBalanceDefaults.VehicleSignalPerVehicle;

        [SettingsUISection(kSection, kSignalsGroup)]
        public bool EnableLiquidWealthSignal { get; set; } = true;

        [SettingsUISection(kSection, kSignalsGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreLiquidWealthSettingsDisabled))]
        [SettingsUISlider(min = 0f, max = 5f, step = 0.05f, scalarMultiplier = 1, unit = Unit.kFloatTwoFractions)]
        public float LiquidWealthSignalSqrtMultiplier { get; set; } = PihBalanceDefaults.LiquidWealthSignalSqrtMultiplier;

        [SettingsUISection(kSection, kSignalsGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreLiquidWealthSettingsDisabled))]
        [SettingsUISlider(min = 0, max = 1000, step = 10, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int LiquidWealthSignalCap { get; set; } = PihBalanceDefaults.LiquidWealthSignalCap;

        [SettingsUISection(kSection, kCacheGroup)]
        public bool EnableHouseholdIncomeCache { get; set; } = true;

        [SettingsUISection(kSection, kCacheGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreCacheSettingsDisabled))]
        [SettingsUISlider(min = 1, max = 64, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int CacheUpdatePartitions { get; set; } = PihBalanceDefaults.CacheUpdatePartitions;

        [SettingsUISection(kSection, kDiagnosticsGroup)]
        public bool EnableDiagnostics { get; set; }

        [SettingsUISection(kSection, kDiagnosticsGroup)]
        [SettingsUIDisableByCondition(typeof(Setting), nameof(AreDiagnosticsSettingsDisabled))]
        [SettingsUISlider(min = 1, max = 8, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        public int DiagnosticsSamplesPerDay { get; set; } = PihBalanceDefaults.DiagnosticsSamplesPerDay;

        public override void SetDefaults()
        {
            EnablePermanentIncomeLayer = true;
            AffordabilityMultiplierPercent = PihBalanceDefaults.AffordabilityMultiplierPercent;
            NonWorkerPotentialIncomeSharePercent = PihBalanceDefaults.NonWorkerPotentialIncomeSharePercent;
            StudentPotentialIncomeSharePercent = PihBalanceDefaults.StudentPotentialIncomeSharePercent;
            StructuralAnchorAdditiveFloor = PihBalanceDefaults.StructuralAnchorAdditiveFloor;
            StructuralAnchorRelativeMarginPercent = PihBalanceDefaults.StructuralAnchorRelativeMarginPercent;
            EnableIncomeSmoothing = true;
            IncomeSmoothingDays = PihBalanceDefaults.IncomeSmoothingDays;
            EnableVehicleSignal = true;
            VehicleSignalPerVehicle = PihBalanceDefaults.VehicleSignalPerVehicle;
            EnableLiquidWealthSignal = true;
            LiquidWealthSignalSqrtMultiplier = PihBalanceDefaults.LiquidWealthSignalSqrtMultiplier;
            LiquidWealthSignalCap = PihBalanceDefaults.LiquidWealthSignalCap;
            EnableHouseholdIncomeCache = true;
            CacheUpdatePartitions = PihBalanceDefaults.CacheUpdatePartitions;
            EnableDiagnostics = false;
            DiagnosticsSamplesPerDay = PihBalanceDefaults.DiagnosticsSamplesPerDay;
        }

        public float GetAffordabilityMultiplier()
        {
            return Math.Max(0.5f, AffordabilityMultiplierPercent / 100f);
        }

        public float GetNonWorkerPotentialIncomeShare()
        {
            return Clamp(NonWorkerPotentialIncomeSharePercent / 100f, 0f, 1f);
        }

        public float GetStudentPotentialIncomeShare()
        {
            return Clamp(StudentPotentialIncomeSharePercent / 100f, 0f, 1f);
        }

        public float GetStructuralAnchorRelativeMarginShare()
        {
            return Clamp(StructuralAnchorRelativeMarginPercent / 100f, 0f, 1f);
        }

        public float GetStructuralAnchorAdditiveFloorAmount()
        {
            return Math.Max(0f, StructuralAnchorAdditiveFloor);
        }

        public float GetLiquidWealthSignalSqrtMultiplier()
        {
            return Math.Max(0f, LiquidWealthSignalSqrtMultiplier);
        }

        private bool AreSmoothingSettingsDisabled() => !EnableIncomeSmoothing;

        private bool AreVehicleSignalSettingsDisabled() => !EnableVehicleSignal;

        private bool AreLiquidWealthSettingsDisabled() => !EnableLiquidWealthSignal;

        private bool AreCacheSettingsDisabled() => !EnableHouseholdIncomeCache;

        private bool AreDiagnosticsSettingsDisabled() => !EnableDiagnostics;

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
                { m_Setting.GetOptionGroupLocaleID(Setting.kStructuralAnchorGroup), "Structural Anchor" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kSmoothingGroup), "Smoothing" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kSignalsGroup), "Signals" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kCacheGroup), "Cache" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kDiagnosticsGroup), "Diagnostics" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnablePermanentIncomeLayer)), "Enable permanent income layer" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnablePermanentIncomeLayer)), "Master switch for the mod's effective-income affordability layer. Disable it when you need a clean vanilla comparison." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.AffordabilityMultiplierPercent)), "Affordability multiplier (%)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.AffordabilityMultiplierPercent)), "Baseline multiplier applied when converting effective income into a housing affordability ceiling." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.NonWorkerPotentialIncomeSharePercent)), "Non-worker potential income share (%)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.NonWorkerPotentialIncomeSharePercent)), "Share of education-implied earning capacity assigned to adult non-workers before unemployment, health, and lifecycle damping." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StudentPotentialIncomeSharePercent)), "Student potential income share (%)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StudentPotentialIncomeSharePercent)), "Share of education-implied earning capacity assigned to adult students before attachment capping." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StructuralAnchorAdditiveFloor)), "Structural anchor additive floor" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StructuralAnchorAdditiveFloor)), "Small flat amount that capped earnings potential may exceed the observed-income base by, even when attachment is weak." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StructuralAnchorRelativeMarginPercent)), "Structural anchor relative margin (%)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StructuralAnchorRelativeMarginPercent)), "Relative overshoot margin applied above observed income after scaling by household attachment." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableIncomeSmoothing)), "Enable income smoothing" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableIncomeSmoothing)), "Uses a slower-moving observed-income baseline so short-run wage shocks do not immediately dominate housing decisions." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.IncomeSmoothingDays)), "Income smoothing window (days)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.IncomeSmoothingDays)), "Trailing window for the smoothed observed-income baseline." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableVehicleSignal)), "Enable vehicle signal" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableVehicleSignal)), "Adds a weak durable-goods signal based on owned vehicles. It stays secondary to the transfer-preserving earnings anchor." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.VehicleSignalPerVehicle)), "Vehicle signal per vehicle" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.VehicleSignalPerVehicle)), "Small per-vehicle adjustment used when the durable-goods signal is enabled." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableLiquidWealthSignal)), "Enable liquid wealth signal" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableLiquidWealthSignal)), "Keeps liquid wealth as a bounded supporting signal rather than the main permanent-income driver." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LiquidWealthSignalSqrtMultiplier)), "Liquid wealth sqrt multiplier" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LiquidWealthSignalSqrtMultiplier)), "Damped multiplier applied to the square root of liquid wealth before the signal cap is applied." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LiquidWealthSignalCap)), "Liquid wealth signal cap" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LiquidWealthSignalCap)), "Hard cap for the bounded liquid-wealth supporting signal." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableHouseholdIncomeCache)), "Enable household cache" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableHouseholdIncomeCache)), "Turns on the shared household-level cache scaffold so multiple systems can read one low-frequency effective-income result." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CacheUpdatePartitions)), "Cache update partitions" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CacheUpdatePartitions)), "How many household partitions the future cache-update pass should rotate through before repeating." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableDiagnostics)), "Enable diagnostics" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableDiagnostics)), "Enables explicit scaffold diagnostics. Until live household integration lands, diagnostics should describe missing integration rather than emit synthetic household samples." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DiagnosticsSamplesPerDay)), "Diagnostics samples per day" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.DiagnosticsSamplesPerDay)), "Reserved cadence for the later live household diagnostics pass." },
            };
        }

        public void Unload()
        {
        }
    }
}
