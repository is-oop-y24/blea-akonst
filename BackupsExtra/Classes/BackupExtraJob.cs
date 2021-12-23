using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Classes.PointCleanupAlgorithms;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Classes
{
    public class BackupExtraJob : BackupJob
    {
        private static ILogger _logger;
        private readonly IPointCleanupAlgorithm _cleanupAlgorithm;

        public BackupExtraJob(string jobName, IRepository repository, IStorageStrategy strategy, IPointCleanupAlgorithm algorithm, ILogger logger)
            : base(jobName, repository, strategy)
        {
            _cleanupAlgorithm = algorithm;
            _logger = logger;
        }

        public override void NewRestorePoint()
        {
            base.NewRestorePoint();

            var restorePoints = GetRestorePoints().ToList();
            List<RestorePoint> result = RemoveRestorePoints.TryToRemove(restorePoints, _cleanupAlgorithm.RestorePointsToCleanup(restorePoints));
            UpdateRestorePoints(result);

            _logger.Write("New restore point has been created!");
        }

        public RestorePoint MergeRestorePoint(RestorePoint oldPoint, RestorePoint newPoint)
        {
            var oldStorages = oldPoint.GetStorages().ToList();
            var newStorages = newPoint.GetStorages().ToList();

            if (oldStorages.TrueForAll(s => s.StoragePath == oldStorages[0].StoragePath))
            {
                return newPoint;
            }

            foreach (Storage oldStorage in oldStorages)
            {
                if (newStorages.Contains(oldStorage))
                {
                    oldStorages.Remove(oldStorage);
                }
            }

            newStorages = newStorages.Union(oldStorages).ToList();

            return new RestorePoint(newPoint.PointNumber, newStorages);
        }

        public void Restore(RestorePoint restorePoint, string restorePath)
        {
            var storages = restorePoint.GetStorages().ToList();

            if (storages.TrueForAll(s => s.StoragePath == storages[0].StoragePath))
            {
                RestoreAlgorithm.RestoreFiles(storages[0].StoragePath, restorePath);
                return;
            }

            foreach (Storage st in storages)
            {
                RestoreAlgorithm.RestoreFiles(st.StoragePath, restorePath);
            }
        }
    }
}