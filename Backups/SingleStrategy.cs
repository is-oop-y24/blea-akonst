using System;

namespace Backups
{
    public class SingleStrategy : IStorageStrategy
    {
        public Tuple<string, string> MakeStorageFile(JobObject obj, RepositoryType type, int restorePointNumber)
        {
            string storageName = obj.FileName
                                 + "_"
                                 + restorePointNumber;
            string storagePath = "./" + obj.JobName
                               + "/" + "RestorePoint_"
                               + restorePointNumber;

            storagePath += ".zip";

            if (type.Equals(RepositoryType.FileSystem))
            {
                FileHandler.ArchivateFile(obj, storagePath);
            }

            return new Tuple<string, string>(storageName, storagePath);
        }
    }
}