using System.IO;
using Backups.Tools;

namespace Backups.FileSystem
{
    class Program
    {
        static void Main()
        {
            const string jobName = "TestJob";
            const string jobsMainDir = "./BackupJobs";
            
            const string fileName1 = "test1.txt";
            const string fileName2 = "test2.txt";
            
            const RepositoryType jobRepoType = RepositoryType.FileSystem;
            const StorageType storageType = StorageType.SingleStorage;

            var backupJob = new BackupJob(jobName, storageType, jobRepoType);
            var jobObject1 = new JobObject(fileName1, jobsMainDir + "/" + fileName1);
            var jobObject2 = new JobObject(fileName2, jobsMainDir + "/" + fileName2);

            backupJob.AddJobObject(jobObject1);
            backupJob.AddJobObject(jobObject2);
            
            backupJob.NewRestorePoint();

            if (!File.Exists("./" + jobName + "RestorePoint_1.zip"))
            {
                throw new BackupsException("File doesn't exists!");
            }
        }
    }
}