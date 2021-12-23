using System.IO.Compression;

namespace BackupsExtra.Classes
{
    public static class RestoreAlgorithm
    {
        public static void RestoreFiles(string storagePath, string restorePath)
        {
            ZipFile.ExtractToDirectory(storagePath, restorePath);
        }
    }
}