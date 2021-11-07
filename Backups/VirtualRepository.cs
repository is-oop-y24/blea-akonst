using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class VirtualRepository : IRepository
    {
        private const RepositoryType Repository = RepositoryType.Virtual;
        public List<Storage> CreateStorages(BackupJob backupJob)
        {
            var storageList = new List<Storage>();
            ReadOnlyCollection<JobObject> jobObjectList = backupJob.GetJobObjects();

            int restorePointNumber = backupJob.CurrentRestorePointNumber();

            foreach (JobObject obj in jobObjectList)
            {
                (string storageName, string storagePath) =
                    backupJob.StorageStrategy.MakeStorageFile(obj, Repository, restorePointNumber);

                storageList.Add(new Storage(storageName, storagePath));
            }

            return storageList;
        }
    }
}