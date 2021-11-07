using System.IO.Compression;

namespace Backups
{
    public class FileHandler
    {
        public void ArchivateFile(bool isArchiveCreated, JobObject obj, string storagePath)
        {
            ZipArchive zipArchive = ZipFile.Open(storagePath, isArchiveCreated ? ZipArchiveMode.Update : ZipArchiveMode.Create);

            zipArchive.CreateEntryFromFile(obj.FilePath, obj.FileName);
        }
    }
}