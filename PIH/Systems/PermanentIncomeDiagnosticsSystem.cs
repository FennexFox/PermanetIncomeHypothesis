using Game;
using PIH.Patches;
using UnityEngine.Scripting;

namespace PIH.Systems
{
    [Preserve]
    public partial class PermanentIncomeDiagnosticsSystem : GameSystemBase
    {
        private bool m_LoggedPatchPlan;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_LoggedPatchPlan = false;
        }

        protected override void OnUpdate()
        {
            if (m_LoggedPatchPlan || Mod.Settings == null || !Mod.Settings.EnableDiagnostics)
            {
                return;
            }

            for (int i = 0; i < PatchPlanRegistry.Targets.Count; i++)
            {
                HousingAffordabilityPatchTarget target = PatchPlanRegistry.Targets[i];
                Mod.log.Info(
                    $"PIH patch target -> system={target.SystemName}, hook={target.HookPoint}, optional={target.Optional}, rationale={target.Rationale}");
            }

            m_LoggedPatchPlan = true;
        }
    }
}

