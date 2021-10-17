using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Course
    {
        private List<Flow> _flowsList = new List<Flow>();
        private char _facultyLetter;

        public Course(string course, string faculty)
        {
            CourseName = course;

            _facultyLetter = faculty switch
            {
                "ИТиП" => 'M',
                "ИКТ" => 'K',
                "ФФ" => 'V',
                "ФТМИ" => 'U',
                "БИТ" => 'N',
                "СУиР" => 'R',
                "БТиНС" => 'B',
                _ => throw new IsuException("Enter correct faculty name, please!")
            };
        }

        public string CourseName { get; }

        public Flow AddFlow(string name, Schedule schedule, int capacity)
        {
            if (_flowsList.Exists(fl => fl.FlowName.Equals(name)))
            {
                throw new IsuException("Flow with this name is already exists!");
            }

            var flow = new Flow(schedule, name, capacity);

            _flowsList.Add(flow);

            return flow;
        }

        public void RemoveFlow(string name)
        {
            Flow flow = _flowsList.FirstOrDefault(fl => fl.FlowName.Equals(name));

            if (flow == default(Flow))
            {
                throw new IsuException("Flow with this name doesn't exists!");
            }

            _flowsList.Remove(flow);
        }

        public Flow GetFlow(string name)
        {
            Flow flow = _flowsList.FirstOrDefault(fl => fl.FlowName.Equals(name));

            if (flow == default(Flow))
            {
                throw new IsuException("Flow with this name doesn't exists!");
            }

            return flow;
        }

        public ReadOnlyCollection<Flow> GetFlows()
        {
            return _flowsList.AsReadOnly();
        }

        public Student AddStudent(Student student, Schedule schedule)
        {
            Flow flow = _flowsList.FirstOrDefault(fl => fl.IsFlowSuitable(schedule));
            if (flow == default(Flow))
            {
                throw new IsuException(
                    "There aren't flows with schedule that will be suitable to your academic schedule!");
            }

            if (student.GroupName.StartsWith(_facultyLetter))
            {
                throw new IsuException("You can't enroll student into course of his faculty!");
            }

            return flow.AddStudentToFlow(student);
        }

        public void RemoveStudent(Student student)
        {
            Flow flow = _flowsList.FirstOrDefault(fl => fl.GetStudentByName(student.Name) == student);
            if (flow == default(Flow))
            {
                throw new IsuException("This student doesn't exists!");
            }

            flow.RemoveStudentFromFlow(student);
        }
    }
}