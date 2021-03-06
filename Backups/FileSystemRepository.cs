using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class FileSystemRepository : IRepository
    {
        private const RepositoryType Repository = RepositoryType.FileSystem;
        public List<Storage> CreateStorages(BackupJob backupJob)
        {
            var storageList = new List<Storage>();
            ReadOnlyCollection<JobObject> jobObjectList = backupJob.GetJobObjects();

            int restorePointNumber = backupJob.CurrentRestorePointNumber();

            foreach (JobObject obj in jobObjectList)
            {
                (string storageName, string storagePath)
                    = backupJob.StorageStrategy.MakeStorageFile(obj, Repository, restorePointNumber);

                FileHandler.ArchivateFile(obj, storagePath);
                storageList.Add(new Storage(storageName, storagePath));
            }

            return storageList;
        }
    }
}