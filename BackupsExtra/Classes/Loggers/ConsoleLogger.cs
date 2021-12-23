using System;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Classes.Loggers
{
    public class ConsoleLogger : ILogger
    {
        private bool _isTimePrefixNeeded;
        public ConsoleLogger(bool isTimePrefixNeeded)
        {
            _isTimePrefixNeeded = isTimePrefixNeeded;
        }

        public void Write(string logData)
        {
            string toPrint = string.Empty;
            if (_isTimePrefixNeeded)
            {
                toPrint += DateTime.Now.ToShortDateString() + " ";
            }

            toPrint += logData;

            Console.WriteLine(toPrint);
        }
    }
}