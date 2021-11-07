namespace Backups
{
    public class JobObject
    {
        public JobObject(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
        }

        public string FilePath { get; }
        public string FileName { get; }
    }
}