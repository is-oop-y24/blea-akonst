using System;

namespace Isu.Tools
{
    public class IsuException : Exception
    {
        public IsuException()
            : base("ISU error occurred!")
        { }

        public IsuException(string message)
            : base(message)
        { }

        public IsuException(Exception innerException)
        {
            throw innerException;
        }
    }
}