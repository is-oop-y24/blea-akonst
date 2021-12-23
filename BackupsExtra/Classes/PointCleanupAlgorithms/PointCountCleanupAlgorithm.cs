using System.Collections.Generic;
using Backups;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Classes.PointCleanupAlgorithms
{
    public class PointCountCleanupAlgorithm : IPointCleanupAlgorithm
    {
        public PointCountCleanupAlgorithm(int limit)
        {
            Limit = limit;
        }

        public int Limit { get; }

        public List<RestorePointNumber> RestorePointsToCleanup(List<RestorePoint> points)
        {
            var resultPointsNumbers = new List<RestorePointNumber>();

            if (points.Count > Limit)
            {
                resultPointsNumbers.Add(new RestorePointNumber(points[0].PointNumber));
            }

            return resultPointsNumbers;
        }
    }
}