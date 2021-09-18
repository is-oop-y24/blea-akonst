using Isu.Tools;

namespace Isu
{
    public class CourseNumber
    {
        private const byte MinCourseNumber = 1;
        private const byte MaxCourseNumber = 4;

        private byte _number;
        public CourseNumber(byte number) => Number = number;
        public byte Number
        {
            get => _number;
            set
            {
                if (value is >= MinCourseNumber and <= MaxCourseNumber)
                    _number = value;
                else
                    throw new IsuException("Course number may be in range from 1 to 4");
            }
        }
    }
}