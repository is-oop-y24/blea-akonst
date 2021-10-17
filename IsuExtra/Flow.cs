using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            _flowSchedule = flowSchedule;
            _capacity = capacity;
        }

        public string FlowName { get; }

        public bool IsFlowSuitable(Schedule schedule)
        {
            return _flowSchedule.IsSuitableSchedules(schedule) && _capacity > _flowStudents.Count;
        }

        public Student AddStudentToFlow(Student student)
        {
            _flowStudents.Add(student);

            return _flowStudents.Find(st => st == student);
        }

        public Student RemoveStudentFromFlow(Student student)
        {
            _flowStudents.Remove(student);

            return _flowStudents.Find(st => st == student);
        }

        public Student GetStudentByName(string name)
        {
            Student student = _flowStudents.FirstOrDefault(st => st.Name.Equals(name));

            return student;
        }

        public ReadOnlyCollection<Student> GetStudents()
        {
            return _flowStudents.AsReadOnly();
        }
    }
}