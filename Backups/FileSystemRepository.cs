using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class FileSystemRepository : IRepository
    {
        public List<Storage> CreateStorages(BackupJob backupJob)
        {
            var storageList = new List<Storage>();
            ReadOnlyCollection<JobObject> jobObjectList = backupJob.GetJobObjects();

            int restorePointNumber = backupJob.CurrentRestorePointNumber();

            foreach (JobObject obj in jobObjectList)
            {
                (string storageName, string storagePath)
                    = backupJob.StorageStrategy.MakeStorageFile(obj, restorePointNumber);

                storageList.Add(new Storage(storageName, storagePath));
            }

            return storageList;
        }
    }
}