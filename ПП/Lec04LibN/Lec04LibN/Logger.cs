using System;
using System.IO;
using Lec04LibN;

namespace Lec04LibN
{
    public class Logger : ILogger
    {
        private readonly string LogFileName;
        private int ID;
        private readonly List<string> Namespaces;
        private static Logger? logger;
        private Logger()
        {
            LogFileName = $"../../../LOG{DateTime.Now:yyyyMMdd-HH-mm-ss}.txt";
            ID = 0;
            Namespaces = [];
        }
        public static ILogger create()
        {
            if (logger is not null)
            {
                return logger;
            }
            logger = new Logger();
            return logger;
        }
        public void start(string title)
        {
            ID++;
            Namespaces.Add(title);
            WriteToFile("STRT", "");
        }
        public void log(string message = "")
        {
            ID++;
            WriteToFile("INFO", message);
        }
        public void stop()
        {
            ID++;
            if (Namespaces.Count > 0)
            {
                Namespaces.RemoveAt(Namespaces.Count - 1);
                WriteToFile("STOP", "");

                return;
            }
            WriteToFile("STOP", "There is no namespace to remove.");
        }
        private void WriteToFile(string logType, string logMessage)
        {
            string logID = ID.ToString("D6");

            string logStringNamespaces = "";
            foreach (string ns in Namespaces)
            {
                logStringNamespaces += $"{ns}:";
            }
            string logString = $"{logID}-{DateTime.Now}-{logType} {logStringNamespaces} {logMessage}";
            if (File.Exists(LogFileName))
            {
                using (StreamWriter str = File.AppendText(LogFileName))
                {
                    str.WriteLine(logString);
                }
                return;
            }
            using (StreamWriter str = File.CreateText(LogFileName))
            {
                str.WriteLine($"{logID}-{DateTime.Now}-INIT");
            }
            ID++;
            WriteToFile(logType, logMessage);
        }
    }
}




