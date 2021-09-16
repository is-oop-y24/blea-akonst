using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu
{
    public class CourseNumber
    {
        private byte _number;
        protected internal CourseNumber(byte number) => Number = number;
        protected internal byte Number
        {
            get => _number;
            set
            {
                if (Enumerable.Range(1, 4).Contains(value))
                    _number = value;
                else
                    throw new IsuException("Course number may be in range from 1 to 4");
            }
        }
    }
}