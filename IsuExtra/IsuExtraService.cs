using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Isu;
using Isu.Tools;
using IsuExtra.Services;

namespace IsuExtra
{
    public class IsuExtraService : IIsuExtraService
    {
        private List<Course> _courses = new List<Course>();
        private Dictionary<Group, Schedule> _schedules = new Dictionary<Group, Schedule>();
        private Dictionary<Student, int> _enrollmentCount = new Dictionary<Student, int>();
        private Dictionary<string, char> _facultyNameToLetterCollection = new Dictionary<string, char>();

        public Course AddCourse(string courseName, string facultyName)
        {
            if (_courses.Exists(cr => cr.CourseName.Equals(courseName)))
            {
                throw new IsuException("This course is exists!");
            }

            if (!_facultyNameToLetterCollection.ContainsKey(facultyName))
            {
                throw new IsuException("Incorrect faculty!");
            }

            var course = new Course(courseName, _facultyNameToLetterCollection[facultyName]);
            _courses.Add(course);

            return course;
        }

        public Student AddStudentToCourse(Student student, Group group, string courseName)
        {
            Course course = _courses.FirstOrDefault(cr => cr.CourseName.Equals(courseName));

            if (course == default(Course))
            {
                throw new IsuException("This course doesn't exists!");
            }

            if (_enrollmentCount.ContainsKey(student))
            {
                if (_enrollmentCount[student] == 2)
                {
                    throw new IsuException("Student is already enrolled to 2 courses!");
                }

                ++_enrollmentCount[student];
            }
            else
            {
                _enrollmentCount.Add(student, 1);
            }

            return course.AddStudent(student, GetGroupSchedule(group));
        }

        public void RemoveStudentFromCourse(Student student, string courseName)
        {
            Course course = _courses.FirstOrDefault(cr => cr.CourseName.Equals(courseName));

            if (course == default(Course))
            {
                throw new IsuException("This course doesn't exists!");
            }

            if (_enrollmentCount[student] == 2)
            {
                --_enrollmentCount[student];
            }
            else
            {
                _enrollmentCount.Remove(student);
            }

            course.RemoveStudent(student);
        }

        public Schedule AddGroupSchedule(Group group, Schedule schedule)
        {
            if (_schedules.ContainsKey(group))
            {
                throw new IsuException("Schedule of this group is already exists!");
            }

            _schedules.Add(group, schedule);
            return _schedules[group];
        }

        public Schedule GetGroupSchedule(Group group)
        {
            if (!_schedules.ContainsKey(group))
            {
                throw new IsuException("Schedule of this group doesn't exists!");
            }

            return _schedules[group];
        }

        public ReadOnlyCollection<Flow> GetCourseFlows(string courseName)
        {
            Course course = _courses.FirstOrDefault(cr => cr.CourseName.Equals(courseName));

            if (course == default(Course))
            {
                throw new IsuException("This course doesn't exists!");
            }

            return course.GetFlows();
        }

        public ReadOnlyCollection<Student> GetStudentsFromFlow(string courseName, string flowName)
        {
            Course course = _courses.FirstOrDefault(cr => cr.CourseName.Equals(courseName));

            if (course == default(Course))
            {
                throw new IsuException("This course doesn't exists!");
            }

            Flow flow = course.GetFlow(flowName);

            return flow.GetStudents();
        }

        public ReadOnlyCollection<Student> GetStudentsWithoutCourseEnrollment(Group group)
        {
            List<Student> groupList = group.ReturnStudentList();
            var studentsWithoutCourse = new List<Student>();

            foreach (Student st in groupList)
            {
                if (!_enrollmentCount.ContainsKey(st))
                {
                    studentsWithoutCourse.Add(st);
                }
            }

            return studentsWithoutCourse.AsReadOnly();
        }

        public void RemoveGroupSchedule(Group group)
        {
            bool result = _schedules.Remove(group);
            if (!result)
            {
                throw new IsuException("Schedule of this group doesn't exists!");
            }
        }

        public void AddFacultyLetterPair(string facultyName, char facultyLetter)
        {
            _facultyNameToLetterCollection.Add(facultyName, facultyLetter);
        }
    }
}