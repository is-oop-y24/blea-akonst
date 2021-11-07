using System;

namespace Backups
{
    public interface IStorageStrategy
    {
        Tuple<string, string> MakeStorageFile(JobObject obj, RepositoryType type, int restorePointNumber);
    }
}