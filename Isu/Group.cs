using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
        private int _capacity;
        private List<Student> _listStudents = new List<Student>();

        // regex that describes the group name in format "letter_3_(1-4)_(00-99)"
        private Regex _validGroupName = new Regex(@"^\w3\d{1,4}\d\d$");
        public Group() { }
        public Group(string name, byte capacity)
        {
            if (_validGroupName.IsMatch(name))
            {
                GroupName = name;
            }
            else
            {
                throw new IsuException("Invalid group name!");
            }

            _capacity = capacity;
            CourseNumber = new CourseNumber(Convert.ToByte(GroupName.Substring(2, 1)));
        }

        public string GroupName { get; }
        public CourseNumber CourseNumber { get; }

        public void AddStudentToGroup(Student student)
        {
            if (_listStudents.Count >= _capacity)
                throw new IsuException("Count of students cannot be more than group capacity!");

            _listStudents.Add(student);
        }

        public void RemoveStudentFromGroup(Student student)
        {
            _listStudents.Remove(student);
        }

        public Student GetStudentById(int id)
        {
            return _listStudents.Find(stud => stud.Id.Equals(id));
        }

        public Student GetStudentByName(string name)
        {
            return _listStudents.Find(stud => stud.Name.Equals(name));
        }

        public List<Student> ReturnStudentList()
        {
            return _listStudents;
        }
    }
}