using System.Collections.Generic;

namespace Backups
{
    public interface IRepository
    {
        public List<Storage> CreateStorages(BackupJob backupJob);
    }
}