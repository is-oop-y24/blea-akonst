using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Course
    {
        private List<Flow> _flowsList = new List<Flow>();

        public Course (string courseName, string facultyName)
        {
            CourseName = courseName;

            FacultyLetter = facultyName switch
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

        public Flow FindSuitableFlow(Schedule schedule)
        {
            Flow flow = _flowsList.FirstOrDefault(fl => fl.)
        }

        public Student AddStudent(Student student, string flowName)
        {
            Flow flow = _flowsList.FirstOrDefault(fl => fl.FlowName.Equals(flowName));
            if (flow == default(Flow))
            {
                throw new IsuException("Flow with this name doesn't exists!");
            }
        }

        public char FacultyLetter;
        public string CourseName { get; }
    }
}