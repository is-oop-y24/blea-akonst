using System;
using System.Collections.Generic;
using Backups;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Classes.PointCleanupAlgorithms
{
    public class DatePointCleanupAlgorithm : IPointCleanupAlgorithm
    {
        public DatePointCleanupAlgorithm(DateTime limit)
        {
            Limit = limit;
        }

        public DateTime Limit { get; }

        public List<RestorePointNumber> RestorePointsToCleanup(List<RestorePoint> points)
        {
            var resultPointsNumbers = new List<RestorePointNumber>();

            foreach (RestorePoint point in points)
            {
                if (point.RestorePointCreateDate > Limit)
                {
                    resultPointsNumbers.Add(new RestorePointNumber(point.PointNumber));
                }
            }

            return resultPointsNumbers;
        }
    }
}