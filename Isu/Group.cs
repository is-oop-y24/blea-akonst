using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
        private int _capacity;
        private List<Student> _listStudents = new List<Student>();
        public Group() { }
        public Group(string name, byte capacity)
        {
            if (name.StartsWith("M3") && name.Length == 5)
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