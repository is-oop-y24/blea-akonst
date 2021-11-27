using System;

namespace Banks.Tools
{
    public class BanksException : Exception
    {
        public BanksException()
            : base("Banks error occurred!")
        { }

        public BanksException(string message)
            : base(message)
        { }

        public BanksException(Exception innerException)
        {
            throw innerException;
        }
    }
}