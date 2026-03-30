using System.Reflection;
using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Simulation;
using HarmonyLib;
using PIH.Systems;

namespace PIH
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(PIH)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        public static Setting Settings { get; private set; }
        public static string ModVersion { get; private set; } = string.Empty;

        private Setting m_Setting;
        private Harmony m_Harmony;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));
            ModVersion = GetAssemblyVersion();

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
            {
                log.Info($"Current mod asset at {asset.path}");
                ModVersion = asset.version != null ? asset.version.ToString() : ModVersion;
            }

            log.Info($"Resolved mod version {ModVersion}");

            updateSystem.UpdateAfter<PermanentIncomeRuntimeSystem, SimulationSystem>(SystemUpdatePhase.GameSimulation);
            updateSystem.UpdateAfter<PermanentIncomeDiagnosticsSystem, SimulationSystem>(SystemUpdatePhase.LateUpdate);

            m_Setting = new Setting(this);
            AssetDatabase.global.LoadSettings(nameof(PIH), m_Setting, new Setting(this));
            Settings = m_Setting;

            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
            m_Setting.RegisterInOptionsUI();

            BootstrapHarmonyPatchesIfNeeded();
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            Settings = null;

            if (m_Harmony != null)
            {
                m_Harmony.UnpatchAll(m_Harmony.Id);
                m_Harmony = null;
            }

            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }

        private void BootstrapHarmonyPatchesIfNeeded()
        {
            try
            {
                m_Harmony = new Harmony(nameof(PIH));
                m_Harmony.PatchAll(typeof(Mod).Assembly);
                log.Info("Harmony patch bootstrap completed.");
            }
            catch (System.Exception ex)
            {
                log.Error($"Harmony patch bootstrap failed. Falling back to scaffold-only behavior for this session. {ex}");
                m_Harmony = null;
            }
        }

        private static string GetAssemblyVersion()
        {
            AssemblyInformationalVersionAttribute informationalVersionAttribute =
                typeof(Mod).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (informationalVersionAttribute != null &&
                !string.IsNullOrWhiteSpace(informationalVersionAttribute.InformationalVersion))
            {
                return informationalVersionAttribute.InformationalVersion.Trim();
            }

            return typeof(Mod).Assembly.GetName().Version?.ToString() ?? string.Empty;
        }
    }
}
