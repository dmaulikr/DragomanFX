using System;
using System.IO;
using DragomanFX.Plugin.Utils;
using UnityInjector;
using UnityInjector.Attributes;

namespace DragomanFX.Plugin
{
    [PluginName("DragomanFX"), PluginVersion("Alpha 0.1")]
    public class DragomanFX : PluginBase
    {
        private static string path;
        public static string FXPath => path ?? (path = GetFXPath());

        public void Awake()
        {
            DontDestroyOnLoad(this);
            Logger.LogToFile = true;
            Logger.LogLine("DragomanFX loaded!");
        }

        private static string GetFXPath()
        {
            string logPath = Path.Combine(Environment.CurrentDirectory, "DragomanFX");
            Logger.LogLine($"DragomanFX Data Path: {logPath}");
            if (Directory.Exists(logPath))
                return logPath;
            try
            {
                Directory.CreateDirectory(logPath);
            }
            catch (Exception)
            {
                Logger.LogLine(LogLevel.Error, "Failed to create DragomanFX directory!");
                return null;
            }

            return logPath;
        }

        public void OnDestroy()
        {
            Logger.LogLine("Closing DragomanFX!");
            if (Logger.LogToFile)
            {
                Logger.LogLine("Saving the log...");
                Logger.SaveLog();
            }
        }
    }
}