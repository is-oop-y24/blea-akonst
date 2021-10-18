using System;

namespace IsuExtra
{
    public class AcademicClass
    {
        public AcademicClass(AcademicClassNumber startAcademicClassNumber, DayOfWeek startDay, string teacherSurname, int audienceNumber)
        {
            StartDay = startDay;
            StartAcademicClassNumber = startAcademicClassNumber;

            TeacherSurname = teacherSurname;
            AudienceNumber = audienceNumber;
        }

        public DayOfWeek StartDay { get; }
        public AcademicClassNumber StartAcademicClassNumber { get; }
        public string TeacherSurname { get; }
        public int AudienceNumber { get; }
    }
}