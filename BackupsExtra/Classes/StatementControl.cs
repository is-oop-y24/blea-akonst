using System.Collections.Generic;
using System.Text.Json;

namespace BackupsExtra.Classes
{
    public static class StatementControl
    {
        public static string SaveJobsPresets(IEnumerable<BackupExtraJob> jobs) => JsonSerializer.Serialize<IEnumerable<BackupExtraJob>>(jobs);
        public static IEnumerable<BackupExtraJob> LoadJobsPresets(string json) => JsonSerializer.Deserialize<IEnumerable<BackupExtraJob>>(json);
    }
}