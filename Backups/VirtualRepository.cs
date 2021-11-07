using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class VirtualRepository : IRepository
    {
        public List<Storage> CreateStorages(BackupJob backupJob)
        {
            var storageList = new List<Storage>();
            ReadOnlyCollection<JobObject> jobObjectList = backupJob.GetJobObjects();

            string storageName, storagePath;

            foreach (JobObject obj in jobObjectList)
            {
                storageName = obj.FileName
                                     + "_"
                                     + backupJob.CurrentRestorePointNumber();
                storagePath = "./" + backupJob.JobName
                                   + "/" + "RestorePoint_"
                                   + backupJob.CurrentRestorePointNumber();
                if (backupJob.StorageType.Equals(StorageType.SplitStorage))
                {
                    storagePath += "/" + storageName;
                }

                storageList.Add(new Storage(storageName, storagePath));
            }

            return storageList;
        }
    }
}