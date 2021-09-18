using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu
{
    public class Group
    {
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

            Capacity = capacity;
            CourseNumber = new CourseNumber(Convert.ToByte(GroupName.Substring(2, 1)));
        }

        public List<Student> ListStudents { get; } = new List<Student>();
        public string GroupName { get; }
        public CourseNumber CourseNumber { get; }
        public byte Capacity { get; }
    }
}