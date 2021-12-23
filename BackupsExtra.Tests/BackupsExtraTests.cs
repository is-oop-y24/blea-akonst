using System.Linq;
using Backups;
using BackupsExtra.Classes;
using BackupsExtra.Classes.Loggers;
using BackupsExtra.Classes.PointCleanupAlgorithms;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class BackupsExtraTests
    {
        public BackupExtraJob Job;

        [SetUp]
        public void Setup()
        {
            var strategy = new SplitStrategy();
            var repository = new VirtualRepository();
            
            int PointsLimit = 5;
            string jobName = "TestJob";
            
            var consoleLogger = new ConsoleLogger(true);
            var cleanupAlgorithm = new PointCountCleanupAlgorithm(PointsLimit);
            
            Job = new BackupExtraJob(jobName, repository, strategy, cleanupAlgorithm, consoleLogger);
            
            var jobObjectA = new JobObject("File_A", "Sample path");
            var jobObjectB = new JobObject("File_B", "Sample path");

            Job.AddJobObject(jobObjectA);
            Job.AddJobObject(jobObjectB);

            Job.NewRestorePoint();

            Job.RemoveJobObject(jobObjectA);

            Job.NewRestorePoint();
            Job.NewRestorePoint();
            Job.NewRestorePoint();
            Job.NewRestorePoint();
        }

        [Test]
        public void AddTwoPointsMoreThanLimit_FirstAndSecondPointWillBeDeleted()
        {
            Job.NewRestorePoint();
            var newPoints = Job.GetRestorePoints().ToList();
            Assert.AreEqual(2, newPoints[0].PointNumber);
            
            Job.NewRestorePoint();
            newPoints = Job.GetRestorePoints().ToList();
            Assert.AreEqual(3, newPoints[0].PointNumber);
        }

        [Test]
        public void MergeTwoPointsWithDiffObjectsCount_ObjectsCountInNewPointIsMoreThanPrevious()
        {
            var jobObjectC = new JobObject("File_C", "Sample path");
            Job.AddJobObject(jobObjectC);
            
            Job.NewRestorePoint();
            var points = Job.GetRestorePoints().ToList();
            
            RestorePoint firstPoint = points[0];
            RestorePoint secondPoint = points[^1];

            RestorePoint resultPoint = Job.MergeRestorePoint(firstPoint, secondPoint);
            
            Assert.AreEqual(secondPoint.GetStorages().Count, resultPoint.GetStorages().Count);
        }
    }
}