using Game;
using UnityEngine.Scripting;

namespace PIH.Systems
{
    [Preserve]
    public partial class HouseholdPermanentIncomeCacheSystem : GameSystemBase
    {
        private int m_CurrentUpdatePartition;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_CurrentUpdatePartition = -1;
        }

        protected override void OnUpdate()
        {
            if (Mod.Settings == null ||
                !Mod.Settings.EnablePermanentIncomeLayer ||
                !Mod.Settings.EnableHouseholdIncomeCache)
            {
                return;
            }

            int partitions = ClampPartitions(Mod.Settings.CacheUpdatePartitions);
            m_CurrentUpdatePartition = (m_CurrentUpdatePartition + 1) % partitions;

            // Live household reads land here later:
            // household members, Citizen, Worker, Student, HealthProblem,
            // observed household income/resources, and optional housing cost.
            // The scaffold intentionally avoids synthetic sample calculations so
            // runtime logs do not imply real simulation integration.
        }

        public int CurrentUpdatePartition => m_CurrentUpdatePartition;

        private static int ClampPartitions(int partitions)
        {
            if (partitions < 1)
            {
                return 1;
            }

            if (partitions > 64)
            {
                return 64;
            }

            return partitions;
        }
    }
}
