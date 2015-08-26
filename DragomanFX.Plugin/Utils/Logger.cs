using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace DragomanFX.Plugin.Utils
{
    public struct LogLevel
    {
        public static LogLevel Info = new LogLevel(ConsoleColor.Blue, "INFO");
        public static LogLevel Warning = new LogLevel(ConsoleColor.Yellow, "WARNING");
        public static LogLevel Error = new LogLevel(ConsoleColor.Red, "ERROR");

        public ConsoleColor Color { get; }
        public string Name { get; }

        public LogLevel(ConsoleColor color, string name)
        {
            Color = color;
            Name = name;
        }
    }

    public static class Logger
    {
        private const string TAG = "DragomanFX";
        private const string DEBUG_FILE_NAME = "dragoman_fx_log.log";
        private const ConsoleColor TAG_COLOR = ConsoleColor.DarkGreen;

        private static TextWriter logFile;

        public static bool LogToFile
        {
            get
            {
                return logFile != null;
            }
            set
            {
                if (value && logFile == null)
                {
                    try
                    {
                        logFile = File.CreateText(Path.Combine(DragomanFX.FXPath, DEBUG_FILE_NAME));
                    }
                    catch (Exception)
                    {
                        logFile = null;
                        LogLine(LogLevel.Error, "Failed to create the log file!");
                    }
                }
                else if (!value) SaveLog();
            }
        }

        public static void LogLine(string msg)
        {
            string tag = $"{TAG}({DateTime.Now.ToString("hh:mm:ss:ff")}): ";

            if (LogToFile)
            {
                logFile.WriteLine(tag + msg);
                logFile.Flush();
            }

            Console.ForegroundColor = TAG_COLOR;
            Console.Write(tag);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(msg);
        }

        public static void LogLine(LogLevel level, string msg)
        {
            string tag = $"{TAG}[{level.Name}]({DateTime.Now.ToString("hh:mm:ss:ff")}): ";

            if (LogToFile)
            {
                logFile.WriteLine(tag + msg);
                logFile.Flush();
            }

            Console.ForegroundColor = level.Color;
            Console.Write(tag);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(msg);
        }

        public static void SaveLog()
        {
            if (!LogToFile)
                return;

            logFile.Flush();
            logFile.Close();
            logFile.Dispose();
            logFile = null;
        }
    }
}
