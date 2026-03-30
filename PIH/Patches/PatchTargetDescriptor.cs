namespace PIH.Patches
{
    public sealed class PatchTargetDescriptor
    {
        public PatchTargetDescriptor(string target, string purpose, string interventionType, string priority)
        {
            Target = target;
            Purpose = purpose;
            InterventionType = interventionType;
            Priority = priority;
        }

        public string Target { get; }
        public string Purpose { get; }
        public string InterventionType { get; }
        public string Priority { get; }
    }
}
