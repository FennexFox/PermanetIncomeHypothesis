using Game;
using UnityEngine.Scripting;

namespace PIH.Systems
{
    [Preserve]
    public partial class PermanentIncomeDiagnosticsSystem : GameSystemBase
    {
        private bool m_LoggedPendingIntegration;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_LoggedPendingIntegration = false;
        }

        protected override void OnUpdate()
        {
            if (m_LoggedPendingIntegration || Mod.Settings == null || !Mod.Settings.EnableDiagnostics)
            {
                return;
            }

            Mod.log.Info(
                "PIH diagnostics scaffold enabled, but live household member/cache integration is not wired yet. No synthetic household sample data is emitted.");
            m_LoggedPendingIntegration = true;
        }
    }
}

