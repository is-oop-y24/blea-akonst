using System;
using System.Collections.ObjectModel;
using Isu;
using Isu.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    [TestFixture]
    public class IsuExtraTest
    {
        private IsuExtraService _service;

        [SetUp]
        public void Setup()
        {
            _service = new IsuExtraService();
        }

        [Test]
        public void CreateNewCourseAndEnrollStudentToHisFacultyCourse_ThrowException()
        {
            Course course = _service.AddCourse("Биотехнологии", "БТиНС");

            var groupSchedule = new Schedule();
            var courseSchedule = new Schedule();
            
            var firstCourseAcademicClass = new AcademicClass(AcademicClassNumber.First, DayOfWeek.Thursday, "Pivovarov", 1222);
            var secondCourseAcademicClass = new AcademicClass(AcademicClassNumber.Second, DayOfWeek.Tuesday, "Vinny", 1223);
            var thirdCourseAcademicClass = new AcademicClass(AcademicClassNumber.Third, DayOfWeek.Friday, "Kashin", 1224);

            var firstAcademicClass = new AcademicClass(AcademicClassNumber.First, DayOfWeek.Monday, "Molochny", 1222);
            var secondAcademicClass = new AcademicClass(AcademicClassNumber.Second, DayOfWeek.Monday, "Shokoladny", 1223);
            var thirdAcademicClass = new AcademicClass(AcademicClassNumber.Third, DayOfWeek.Monday, "Smirnov", 1224);
            
            groupSchedule.AddAcademicClass(firstAcademicClass);
            groupSchedule.AddAcademicClass(secondAcademicClass);
            groupSchedule.AddAcademicClass(thirdAcademicClass);
            
            courseSchedule.AddAcademicClass(firstCourseAcademicClass);
            courseSchedule.AddAcademicClass(secondCourseAcademicClass);
            courseSchedule.AddAcademicClass(thirdCourseAcademicClass);

            course.AddFlow("1.1", courseSchedule, 20);
            
            var group = new Group("B3110", 20);
            var student = new Student(group, "Vasiliy");

            _service.AddGroupSchedule(group, groupSchedule);
            Assert.Catch<IsuException>(() => _service.AddStudentToCourse(student, group, "Биотехнологии"));
        }

        [Test]
        public void StudentEnrollsToCourseAndLeaves_StudentEnrollmentIsFalse()
        {
            Course course = _service.AddCourse("Биотехнологии", "БТиНС");

            var groupSchedule = new Schedule();
            var courseSchedule = new Schedule();
            
            var firstCourseAcademicClass = new AcademicClass(AcademicClassNumber.First, DayOfWeek.Thursday, "Pivovarov", 1222);
            var secondCourseAcademicClass = new AcademicClass(AcademicClassNumber.Second, DayOfWeek.Tuesday, "Vinny", 1223);
            var thirdCourseAcademicClass = new AcademicClass(AcademicClassNumber.Third, DayOfWeek.Friday, "Kashin", 1224);

            var firstAcademicClass = new AcademicClass(AcademicClassNumber.First, DayOfWeek.Monday, "Molochny", 1222);
            var secondAcademicClass = new AcademicClass(AcademicClassNumber.Second, DayOfWeek.Monday, "Shokoladny", 1223);
            var thirdAcademicClass = new AcademicClass(AcademicClassNumber.Third, DayOfWeek.Monday, "Smirnov", 1224);
            
            groupSchedule.AddAcademicClass(firstAcademicClass);
            groupSchedule.AddAcademicClass(secondAcademicClass);
            groupSchedule.AddAcademicClass(thirdAcademicClass);
            
            courseSchedule.AddAcademicClass(firstCourseAcademicClass);
            courseSchedule.AddAcademicClass(secondCourseAcademicClass);
            courseSchedule.AddAcademicClass(thirdCourseAcademicClass);

            course.AddFlow("1.1", courseSchedule, 20);
            
            var group = new Group("M3110", 20);
            var student = new Student(group, "Vasiliy");

            group.AddStudentToGroup(student);

            _service.AddGroupSchedule(group, groupSchedule);

            _service.AddStudentToCourse(student, group, "Биотехнологии");
            _service.RemoveStudentFromCourse(student, "Биотехнологии");
            
            ReadOnlyCollection<Student> withoutCourseList = _service.GetStudentsWithoutCourseEnrollment(group);

            Assert.IsTrue(withoutCourseList.Contains(student));
        }
        
        [Test]
        public void StudentTriesToEnrollIntoMoreThanOneCourse_ThrowException()
        {
            Course course = _service.AddCourse("Биотехнологии", "БТиНС");
            Course course2 = _service.AddCourse("Компьютерные технологии", "ИТиП");

            var groupSchedule = new Schedule();
            var courseSchedule = new Schedule();
            
            var firstCourseAcademicClass = new AcademicClass(AcademicClassNumber.First, DayOfWeek.Thursday, "Pivovarov", 1222);
            var secondCourseAcademicClass = new AcademicClass(AcademicClassNumber.Second, DayOfWeek.Tuesday, "Vinny", 1223);
            var thirdCourseAcademicClass = new AcademicClass(AcademicClassNumber.Third, DayOfWeek.Friday, "Kashin", 1224);

            var firstAcademicClass = new AcademicClass(AcademicClassNumber.First, DayOfWeek.Monday, "Molochny", 1222);
            var secondAcademicClass = new AcademicClass(AcademicClassNumber.Second, DayOfWeek.Monday, "Shokoladny", 1223);
            var thirdAcademicClass = new AcademicClass(AcademicClassNumber.Third, DayOfWeek.Monday, "Smirnov", 1224);
            
            groupSchedule.AddAcademicClass(firstAcademicClass);
            groupSchedule.AddAcademicClass(secondAcademicClass);
            groupSchedule.AddAcademicClass(thirdAcademicClass);
            
            courseSchedule.AddAcademicClass(firstCourseAcademicClass);
            courseSchedule.AddAcademicClass(secondCourseAcademicClass);
            courseSchedule.AddAcademicClass(thirdCourseAcademicClass);

            course.AddFlow("1.1", courseSchedule, 20);
            
            firstCourseAcademicClass = new AcademicClass(AcademicClassNumber.First, DayOfWeek.Thursday, "Torvalds", 1222);
            secondCourseAcademicClass = new AcademicClass(AcademicClassNumber.Second, DayOfWeek.Tuesday, "Gates", 1223);
            thirdCourseAcademicClass = new AcademicClass(AcademicClassNumber.Third, DayOfWeek.Friday, "Straustrup", 1224);

            courseSchedule = new Schedule();
            
            courseSchedule.AddAcademicClass(firstCourseAcademicClass);
            courseSchedule.AddAcademicClass(secondCourseAcademicClass);
            courseSchedule.AddAcademicClass(thirdCourseAcademicClass);
            
            course2.AddFlow("1.1", courseSchedule, 20);
            
            var group = new Group("P3110", 20);
            var student = new Student(group, "Vasiliy");

            group.AddStudentToGroup(student);

            _service.AddGroupSchedule(group, groupSchedule);

            _service.AddStudentToCourse(student, group, "Биотехнологии");

            Assert.Catch<IsuException>(() => _service.AddStudentToCourse(student, group, "Компьютерные технологии"));
        }
        
        [Test]
        public void GroupScheduleCrossesWithCourseSchedule_ThrowException()
        {
            Course course = _service.AddCourse("Биотехнологии", "БТиНС");

            var groupSchedule = new Schedule();
            var courseSchedule = new Schedule();
            
            var academicClass = new AcademicClass(AcademicClassNumber.First, DayOfWeek.Thursday, "Pivovarov", 1222);

            groupSchedule.AddAcademicClass(academicClass);
            courseSchedule.AddAcademicClass(academicClass);

            course.AddFlow("1.1", courseSchedule, 20);

            var group = new Group("P3110", 20);
            var student = new Student(group, "Vasiliy");

            group.AddStudentToGroup(student);

            _service.AddGroupSchedule(group, groupSchedule);
            Assert.Catch<IsuException>(() => _service.AddStudentToCourse(student, group, "Биотехнологии"));
        }
    }
}