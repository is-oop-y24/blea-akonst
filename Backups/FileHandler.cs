using System.IO;
using System.IO.Compression;

namespace Backups
{
    public static class FileHandler
    {
        public static void ArchivateFile(JobObject obj, string storagePath)
        {
            ZipArchive zipArchive = ZipFile.Open(storagePath, File.Exists(storagePath) ? ZipArchiveMode.Update : ZipArchiveMode.Create);

            zipArchive.CreateEntryFromFile(obj.FilePath, obj.FileName);
        }
    }
}