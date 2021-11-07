using System;

namespace Backups.Tools
{
    public class BackupsException : Exception
    {
        public BackupsException()
            : base("Backups error occurred!")
        { }

        public BackupsException(string message)
            : base(message)
        { }

        public BackupsException(Exception innerException)
        {
            throw innerException;
        }
    }
}