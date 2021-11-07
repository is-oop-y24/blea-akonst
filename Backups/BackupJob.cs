using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups.Tools;

namespace Backups
{
    public class BackupJob
    {
        private static IRepository _repository;
        private int _currentRestorePointNumber = 1;

        private List<JobObject> _jobObjects = new List<JobObject>();
        private List<RestorePoint> _restorePoints = new List<RestorePoint>();

        public BackupJob()
        {
            throw new BackupsException("You should provide job name and type of file storaging!");
        }

        public BackupJob(string jobName, StorageType storageType, RepositoryType repositoryType)
        {
            JobName = jobName;
            StorageType = storageType;
            _repository = repositoryType switch
            {
                RepositoryType.Virtual => new VirtualRepository(),
                RepositoryType.FileSystem => new FileSystemRepository(),
                _ => throw new BackupsException("Incorrect repository type!")
            };
        }

        public string JobName { get; }
        public StorageType StorageType { get; }

        public void AddJobObject(JobObject jobObject)
        {
            if (_jobObjects.Contains(jobObject))
            {
                throw new BackupsException("This job object is already exists!");
            }

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

        public void NewRestorePoint()
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
    }
}