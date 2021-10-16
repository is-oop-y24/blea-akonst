using System;

namespace IsuExtra
{
    public enum CoupleNumber : byte
    {
        /// <summary> 1st couple </summary>
        First = 1,

        /// <summary> 2nd couple </summary>
        Second,

        /// <summary> 3rd couple </summary>
        Third,

        /// <summary> 4th couple </summary>
        Fourth,

        /// <summary> 5th couple </summary>
        Fifth,

        /// <summary> 6th couple </summary>
        Sixth,

        /// <summary> 7th couple </summary>
        Seventh,
    }

    public class Couple
    {
        public Couple(CoupleNumber startCoupleNumber, DayOfWeek startDay, string teacherSurname, int audienceNumber)
        {
            StartDay = startDay;
            StartCoupleNumber = startCoupleNumber;

            TeacherSurname = teacherSurname;
            AudienceNumber = audienceNumber;
        }

        public DayOfWeek StartDay { get; }
        public CoupleNumber StartCoupleNumber { get; }
        public string TeacherSurname { get; }
        public int AudienceNumber { get; }
    }
}