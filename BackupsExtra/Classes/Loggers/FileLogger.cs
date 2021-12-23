using System;
using System.IO;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Classes.Loggers
{
    public class FileLogger : ILogger
    {
        private bool _isTimePrefixNeeded;
        public FileLogger(bool isTimePrefixNeeded)
        {
            _isTimePrefixNeeded = isTimePrefixNeeded;
        }

        public void Write(string logData)
        {
            string currentDate = DateTime.Now.ToShortDateString();

            string logFileName = currentDate + "_BackupsExtra.log";

            string stringToWrite = string.Empty;

            using var fs = new FileStream(logFileName, FileMode.OpenOrCreate);
            if (_isTimePrefixNeeded)
            {
                stringToWrite += currentDate + " ";
            }

            stringToWrite += logData;

            fs.Write(System.Text.Encoding.Default.GetBytes(stringToWrite), 0, stringToWrite.Length);

            fs.Close();
        }
    }
}