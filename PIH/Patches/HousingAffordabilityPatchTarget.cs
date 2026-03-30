namespace PIH.Patches
{
    public sealed class HousingAffordabilityPatchTarget
    {
        public HousingAffordabilityPatchTarget(string systemName, string hookPoint, string rationale, bool optional)
        {
            SystemName = systemName;
            HookPoint = hookPoint;
            Rationale = rationale;
            Optional = optional;
        }

        public string SystemName { get; }

        public string HookPoint { get; }

        public string Rationale { get; }

        public bool Optional { get; }
    }
}

