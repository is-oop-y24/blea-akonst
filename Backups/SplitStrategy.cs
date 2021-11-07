using System;

namespace Backups
{
    public class SplitStrategy : IStorageStrategy
    {
        public Tuple<string, string> MakeStorageFile(JobObject obj, RepositoryType type, int restorePointNumber)
        {
            string storageName = obj.FileName
                                 + "_"
                                 + restorePointNumber;
            string storagePath = "./" + obj.JobName
                               + "/" + "RestorePoint_"
                               + restorePointNumber;

            storagePath += "/" + storageName + ".zip";

            return new Tuple<string, string>(storageName, storagePath);
        }
    }
}