using System;
using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Classes.PointCleanupAlgorithms
{
    public class HybridPointCleanupAlgorithm : IPointCleanupAlgorithm
    {
        private PointCountCleanupAlgorithm _pointCountCleanupAlgorithm;
        private DatePointCleanupAlgorithm _datePointCleanupAlgorithm;

        public HybridPointCleanupAlgorithm(DateTime limitDateTime, int limitCount, bool isBothLimitsUsageRequired)
        {
            _pointCountCleanupAlgorithm = new PointCountCleanupAlgorithm(limitCount);
            _datePointCleanupAlgorithm = new DatePointCleanupAlgorithm(limitDateTime);

            BothLimitsUsageRequired = isBothLimitsUsageRequired;
        }

        public bool BothLimitsUsageRequired { get; }

        public List<RestorePointNumber> RestorePointsToCleanup(List<RestorePoint> points)
        {
            List<RestorePointNumber> pointsToDeleteInCountAlgorithm =
                _pointCountCleanupAlgorithm.RestorePointsToCleanup(points);

            List<RestorePointNumber> pointsToDeleteInDateAlgorithm =
                _datePointCleanupAlgorithm.RestorePointsToCleanup(points);

            if (BothLimitsUsageRequired)
            {
                return pointsToDeleteInCountAlgorithm
                    .Union(pointsToDeleteInDateAlgorithm, new RestorePointNumberComparer()).ToList();
            }

            if (pointsToDeleteInCountAlgorithm.Count > 0)
            {
                return pointsToDeleteInCountAlgorithm;
            }

            return pointsToDeleteInDateAlgorithm;
        }
    }
}