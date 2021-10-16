using System;
using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Flow
    {
        private byte _capacity;
        private Schedule _flowSchedule;
        private List<Student> _flowStudents = new List<Student>();

        public bool IsFlowSuitable(Schedule schedule)
        {
            
        }

        public Flow(Schedule flowSchedule, string name, byte capacity)
        {
            FlowName = name;
            if (flowSchedule.CoupleCount() != 3)
            {
                throw new IsuException("Count of couples must be 3! (1 lecture, 2 practices)");
            }

            _flowSchedule = flowSchedule;
            _capacity = capacity;
        }

        public Student AddStudentToFlow(Student student)
        {
            _flowStudents.Add(student);
        }

        public Student GetStudentByName(string name)
        {
            Student student = _flowStudents.FirstOrDefault(st => st.Name.Equals(name));

            if (student == default(Student))
            {
                throw new IsuException("This student doesn't exists!");
            }

            return _flowStudents.FirstOrDefault();
        }

        public string FlowName { get; }
    }
}