using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Flow
    {
        private List<Student> _flowStudents = new List<Student>();
        private int _capacity;
        private Schedule _flowSchedule;

        public Flow(Schedule flowSchedule, string name, int capacity)
        {
            FlowName = name;
            if (flowSchedule.CoupleCount() != 3)
            {
                throw new IsuException("Count of couples must be 3! (1 lecture, 2 practices)");
            }

            _flowSchedule = flowSchedule;
            _capacity = capacity;
        }

        public string FlowName { get; }

        public bool IsFlowSuitable(Schedule schedule)
        {
            return _flowSchedule.IsSuitableSchedules(schedule);
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
    }
}