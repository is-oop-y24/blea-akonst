using System;

namespace Backups
{
    public class SingleFileStrategy : IStorageStrategy
    {
        public Tuple<string, string> MakeStorageFile(JobObject obj, int restorePointNumber)
        {
            string storageName = obj.FileName
                                 + "_"
                                 + restorePointNumber;
            string storagePath = "./" + obj.JobName
                               + "/" + "RestorePoint_"
                               + restorePointNumber;

            storagePath += ".zip";
            FileHandler.ArchivateFile(obj, storagePath);

            return new Tuple<string, string>(storageName, storagePath);
        }
    }
}