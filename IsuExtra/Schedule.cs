using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Tools;

namespace IsuExtra
{
    public class Schedule
    {
        private List<AcademicClass> _schedule = new List<AcademicClass>();

        public void AddAcademicClass(AcademicClass academicClass)
        {
            if (_schedule.Exists(cp => cp.StartAcademicClassNumber.Equals(academicClass.StartAcademicClassNumber)
                                                && cp.StartDay.Equals(academicClass.StartDay)))
            {
                throw new IsuException("This time is busy!");
            }

            _schedule.Add(academicClass);
        }

        public bool RemoveAcademicClass(AcademicClass academicClass)
        {
            return _schedule.Remove(academicClass);
        }

        public bool IsTimeBusy(AcademicClass academicClass)
        {
            AcademicClass result = _schedule.FirstOrDefault(c => c.StartDay.Equals(academicClass.StartDay)
                                                          && c.StartAcademicClassNumber.Equals(academicClass.StartAcademicClassNumber));

            return result != default(AcademicClass);
        }

        public bool IsSuitableSchedules(Schedule schedule)
        {
            foreach (AcademicClass cp in schedule._schedule)
            {
                AcademicClass patternAcademicClass = _schedule.FirstOrDefault(academicClass =>
                                                                academicClass.StartDay.Equals(cp.StartDay) &&
                                                                academicClass.StartAcademicClassNumber.Equals(cp.StartAcademicClassNumber));

                if (patternAcademicClass != default)
                {
                    return false;
                }
            }

            return true;
        }

        public int AcademicClassCount()
        {
            return _schedule.Count;
        }
    }
}