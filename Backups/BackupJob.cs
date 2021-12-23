using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups.Tools;

namespace Backups
{
    public class BackupJob
    {
        private List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private IRepository _repository;
        private int _currentRestorePointNumber = 1;

        private List<JobObject> _jobObjects = new List<JobObject>();

        public BackupJob(string jobName, IRepository repository, IStorageStrategy strategy)
        {
            JobName = jobName;
            _repository = repository;
            StorageStrategy = strategy;
        }

        public string JobName { get; }
        public IStorageStrategy StorageStrategy { get; }

        public void AddJobObject(JobObject jobObject)
        {
            if (_jobObjects.Contains(jobObject))
            {
                throw new BackupsException("This job object is already exists!");
            }

            jobObject.JobName = JobName;

            _jobObjects.Add(jobObject);
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            if (!_jobObjects.Contains(jobObject))
            {
                throw new BackupsException("This job object doesn't exists!");
            }

            _jobObjects.Remove(jobObject);
        }

        public virtual void NewRestorePoint()
        {
            List<Storage> storages = _repository.CreateStorages(this);

            var restorePoint = new RestorePoint(_currentRestorePointNumber++, storages);
            _restorePoints.Add(restorePoint);
        }

        public ReadOnlyCollection<JobObject> GetJobObjects()
        {
            return _jobObjects.AsReadOnly();
        }

        public ReadOnlyCollection<RestorePoint> GetRestorePoints()
        {
            return _restorePoints.AsReadOnly();
        }

        public int CurrentRestorePointNumber()
        {
            return _currentRestorePointNumber;
        }

        protected void UpdateRestorePoints(List<RestorePoint> points)
        {
            _restorePoints = points;
        }
    }
}