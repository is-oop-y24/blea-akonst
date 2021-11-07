using System.Collections.Generic;

namespace Backups
{
    public interface IRepository
    {
        List<Storage> CreateStorages(BackupJob backupJob);
    }
}