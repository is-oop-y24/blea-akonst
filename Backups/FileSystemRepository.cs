using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class FileSystemRepository : IRepository
    {
        private static FileHandler _fileHandler = new FileHandler();
        public List<Storage> CreateStorages(BackupJob backupJob)
        {
            var storageList = new List<Storage>();
            ReadOnlyCollection<JobObject> jobObjectList = backupJob.GetJobObjects();

            string storageName, storagePath;
            int restorePointNumber = backupJob.CurrentRestorePointNumber();

            bool isArchiveCreated = false;

            foreach (JobObject obj in jobObjectList)
            {
                storageName = obj.FileName
                              + "_"
                              + restorePointNumber;
                storagePath = "./" + backupJob.JobName
                                   + "/" + "RestorePoint_"
                                   + restorePointNumber;

                if (backupJob.StorageType.Equals(StorageType.SplitStorage))
                {
                    storagePath += "/" + storageName + ".zip";
                    _fileHandler.ArchivateFile(isArchiveCreated, obj, storagePath);
                }
                else if (backupJob.StorageType.Equals(StorageType.SingleStorage))
                {
                    storagePath += ".zip";
                    _fileHandler.ArchivateFile(isArchiveCreated, obj, storagePath);
                    isArchiveCreated = true;
                }

                storageList.Add(new Storage(storageName, storagePath));
            }

            return storageList;
        }
    }
}