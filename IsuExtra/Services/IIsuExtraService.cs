using System.Collections.ObjectModel;
using Isu;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Course AddCourse(string courseName, string facultyName);
        Student AddStudentToCourse(Student student, Group group, string courseName);
        Schedule AddGroupSchedule(Group group, Schedule schedule);
        Schedule GetGroupSchedule(Group group);
        ReadOnlyCollection<Flow> GetCourseFlows(string courseName);
        ReadOnlyCollection<Student> GetStudentsFromFlow(string courseName, string flowName);
        ReadOnlyCollection<Student> GetStudentsWithoutCourseEnrollment(Group group);
        void RemoveGroupSchedule(Group group);
        void RemoveStudentFromCourse(Student student, string courseName);
    }
}