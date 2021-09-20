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
            Group searchResultGroup = FindGroup(group.GroupName);

            if (searchResultGroup.Equals(new Group()))
                throw new IsuException("Group with this name not found!");

            searchResultGroup.AddStudentToGroup(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (Group g in _isuGroups)
            {
                Student searchResultStudent = g.GetStudentById(id);
                if (searchResultStudent != null && !searchResultStudent.Equals(new Student()))
                    return searchResultStudent;
            }

            throw new IsuException("Student with this ISU number not found!");
        }

        public Student FindStudent(string name)
        {
            foreach (Group g in _isuGroups)
            {
                Student searchResultStudent = g.GetStudentByName(name);
                if (searchResultStudent != null && !searchResultStudent.Equals(new Student()))
                    return searchResultStudent;
            }

            throw new IsuException("Student with this name not found!");
        }

        public List<Student> FindStudents(string groupName)
        {
            Group searchResult = _isuGroups.Find(gr => gr.GroupName.Equals(groupName));
            if (searchResult != null && !searchResult.Equals(new Group()))
            {
                return searchResult.ReturnStudentList();
            }

            throw new IsuException("Group was not found!");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var searchResultList = new List<Student>();
            foreach (Group g in _isuGroups)
            {
                if (g.CourseNumber.Equals(courseNumber))
                {
                    searchResultList.AddRange(g.ReturnStudentList());
                }
            }

            if (!searchResultList.Count.Equals(0))
            {
                return searchResultList;
            }
            else
            {
                throw new IsuException("Groups with this course number was not found!");
            }
        }

        public Group FindGroup(string groupName)
        {
            Group searchResultGroup = _isuGroups.Find(gr => gr.GroupName.Equals(groupName));
            if (searchResultGroup != null && !searchResultGroup.Equals(new Group()))
            {
                return searchResultGroup;
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

            FindGroup(student.GroupName).RemoveStudentFromGroup(student);
            FindGroup(newGroup.GroupName).AddStudentToGroup(student);
        }
    }
}