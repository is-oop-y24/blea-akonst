using System;

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
        public string JobName { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((JobObject)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FilePath, FileName);
        }

        private bool Equals(JobObject other)
        {
            return FilePath == other.FilePath && FileName == other.FileName;
        }
    }
}