using System.Collections.Generic;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace Isu
{
    public class IsuService : IIsuService
    {
        private readonly List<Group> _isuGroups = new List<Group>();

        public Group AddGroup(string name, byte limit)
        {
            if (_isuGroups.Exists(group => group.GroupName.Equals(name)))
            {
                throw new IsuException("Group with this name is already exists!");
            }

            var group = new Group(name, limit);
            _isuGroups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(group, name);
            foreach (Group g in _isuGroups.Where(g => g.GroupName.Equals(@group.GroupName)))
            {
                if (g.ListStudents.Count < g.Capacity)
                {
                    g.ListStudents.Add(student);
                    return student;
                }
                else
                {
                    throw new IsuException("Count of students cannot be more than group capacity!");
                }
            }

            throw new IsuException("Group with this name not found!");
        }

        public Student GetStudent(int id)
        {
            foreach (Group g in _isuGroups)
            {
                foreach (Student student in g.ListStudents)
                {
                    if (student.Id.Equals(id))
                    {
                        return student;
                    }
                }
            }

            throw new IsuException("Student with this ISU number not found!");
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in from @group in _isuGroups from student in @group.ListStudents where student.Name.Equals(name) select student)
            {
                return student;
            }

            throw new IsuException("Student with this name not found!");
        }

        public List<Student> FindStudents(string groupName)
        {
            var searchResultList = _isuGroups.Where(@group => @group.GroupName.Equals(groupName)).SelectMany(@group => @group.ListStudents).ToList();
            return searchResultList;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var searchResultList = _isuGroups.Where(@group => @group.CourseNumber.Equals(courseNumber)).SelectMany(@group => @group.ListStudents).ToList();
            return searchResultList;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group @group in _isuGroups.Where(@group => @group.GroupName.Equals(groupName)))
            {
                return @group;
            }

            throw new IsuException("Group with this name not found!");
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _isuGroups.Where(@group => @group.CourseNumber.Equals(courseNumber)).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (!student.CourseNumber.Equals(newGroup.CourseNumber) || student.GroupName.Equals(newGroup.GroupName))
            {
                throw new IsuException("You can only change a group a student within one course!");
            }
            else
            {
                FindGroup(student.GroupName).ListStudents.Remove(student);
                FindGroup(newGroup.GroupName).ListStudents.Add(student);
            }
        }
    }
}