namespace BackupsExtra.Enums
{
    public enum PointCleanupAlgorithmType
    {
        /// <summary>Point count restore points cleanup algorithm</summary>
        PointCount,

        /// <summary>Date of restore points create cleanup algorithm</summary>
        PointDate,

        /// <summary>Hybrid restore points cleanup algorithm</summary>
        Hybrid,
    }
}