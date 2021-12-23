using System.Collections.Generic;
using Backups;
using BackupsExtra.Classes.PointCleanupAlgorithms;

namespace BackupsExtra.Interfaces
{
    public interface IPointCleanupAlgorithm
    {
        List<RestorePointNumber> RestorePointsToCleanup(List<RestorePoint> points);
    }
}