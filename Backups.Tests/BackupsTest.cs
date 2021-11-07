using System.Collections.ObjectModel;
using NUnit.Framework;

namespace Backups.Tests
{
    [TestFixture]
    public class BackupsTest
    {
        public string JobName = "TestJob";
        public RepositoryType JobRepoType = RepositoryType.Virtual;
        public SplitVirtualStrategy Strategy = new SplitVirtualStrategy();
        
        [Test]
        public void CreateTwoRestorePointsWithDiffJobObjCount_StoragesCountsAreDiff()
        {
            var virtualRepository = new VirtualRepository();
            var backupJob = new BackupJob(JobName, virtualRepository, Strategy);
            var jobObjectA = new JobObject("File_A", "Sample path");
            var jobObjectB = new JobObject("File_B", "Sample path");
            
            backupJob.AddJobObject(jobObjectA);
            backupJob.AddJobObject(jobObjectB);

            backupJob.NewRestorePoint();

            backupJob.RemoveJobObject(jobObjectA);

            backupJob.NewRestorePoint();

            ReadOnlyCollection<RestorePoint> restorePoints = backupJob.GetRestorePoints();
            
            Assert.AreEqual(2, restorePoints.Count);
            Assert.AreEqual(1, restorePoints[0].PointNumber);
            Assert.AreEqual(2, restorePoints[1].PointNumber);

            ReadOnlyCollection<Storage> firstPointStorages = restorePoints[0].GetStorages();
            ReadOnlyCollection<Storage> secondPointStorages = restorePoints[1].GetStorages();
                
            Assert.AreEqual(2, firstPointStorages.Count);
            Assert.AreEqual(1, secondPointStorages.Count);
        }
    }
}