using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DragomanFX.Plugin.FXParser.Commands;
using DragomanFX.Plugin.Utils;

namespace DragomanFX.Plugin.FXParser
{
    public class Recipe
    {
        private readonly Regex removeCommentsPattern = new Regex(@"(//.*|(?s:/\*(?((?<!\*/)).|\*/)*))");

        private readonly List<Command> commands;

        public Recipe(string path)
        {
            commands = new List<Command>();
            Name = Path.GetFileName(path);
            LoadRecipe(path);
        }

        public string Name { get; }

        private void LoadRecipe(string path)
        {
            string filePath = Path.IsPathRooted(path) ? path : Path.Combine(DragomanFX.FXPath, path);
            Logger.LogLine($"Attempting to load recipe {Name}: {filePath}");
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Failed to find the recipe file!", filePath);
            if (Path.GetExtension(filePath) != ".h")
                throw new FileNotFoundException("The file must have .h extension!", filePath);

            string shader;
            try
            {
                using (TextReader tr = File.OpenText(filePath))
                {
                    shader = tr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Logger.LogLine(
                LogLevel.Error,
                $"Failed to load the recipe {Name}. Is the file protected from reading operations?");
                Logger.LogLine(
                LogLevel.Error,
                $"Stats for devs:\nType: {e.GetType()}\nMessage: {e.Message}\nStack trace: {e.StackTrace}");
                return;
            }

            shader = removeCommentsPattern.Replace(shader, string.Empty);
            StringReader sr = new StringReader(shader.Trim());
            string line;
            while ((line = sr.ReadLine()?.Trim()) != null)
            {
                if(line == string.Empty) continue;
                try
                {
                    Command command = CommandCollection.CreateCommand(this, line);
                    if(command != null)
                        commands.Add(command);
                    else
                        Logger.LogLine(LogLevel.Error, $"Failed to parse line: {line}!");
                }
                catch (Exception)
                {
                        Logger.LogLine(LogLevel.Error, $"Failed to parse line: {line}! Failed to transform into primitive type!");
                }
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"RECIPE[{Name}]({commands.Count} values):\n");
            foreach (Command command in commands)
            {
                sb.AppendLine(command.ToString());
            }
            return sb.ToString();
        }
    }
}