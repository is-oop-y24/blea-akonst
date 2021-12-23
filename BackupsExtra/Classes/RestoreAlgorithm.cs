using System.IO.Compression;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Classes
{
    public class RestoreAlgorithm : IRestoreAlgorithm
    {
        public void RestoreFiles(string storagePath, string restorePath)
        {
            ZipFile.ExtractToDirectory(storagePath, restorePath);
        }
    }
}