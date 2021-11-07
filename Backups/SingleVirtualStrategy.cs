using System;

namespace Backups
{
    public class SingleVirtualStrategy : IStorageStrategy
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

            return new Tuple<string, string>(storageName, storagePath);
        }
    }
}