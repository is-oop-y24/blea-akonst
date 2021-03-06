using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3109", 20);
            _isuService.AddStudent(group, "Vasya");
            Student student = _isuService.FindStudent("Vasya");
            Assert.AreEqual(group.GroupName, student.GroupName);                                // student has group
            Assert.AreEqual(student, _isuService.FindGroup("M3109").GetStudentByName("Vasya")); // group in ISU has student Vasya
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group group = _isuService.AddGroup("M3109", 20);
            Assert.Catch<IsuException>(() =>
            {
                for (int i = 0; i < 21; ++i)
                {
                    _isuService.AddStudent(group, "Vasiliy");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("Group of Blood", 13);
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group firstYearGroup = _isuService.AddGroup("M3109", 20);
            Group secondYearGroup = _isuService.AddGroup("M3209", 20);
            Student student = _isuService.AddStudent(firstYearGroup, "Vasya");

            Assert.Catch<IsuException>(() =>
            {
                _isuService.ChangeStudentGroup(student, secondYearGroup);
            });
        }
    }
}