namespace BackupsExtra.Interfaces
{
    public interface IRestoreAlgorithm
    {
        void RestoreFiles(string storagePath, string restorePath);
    }
}