using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Commands
{
    public class CommandDocs : Command
    {
        private static readonly Regex Pattern = new Regex("(?<=\").+(?=\")");

        public CommandDocs(string parameters, Recipe recipe) : base(parameters, recipe)
        {
            Match m = Pattern.Match(parameters.Trim());
            if (!m.Success)
                throw new ArgumentException(
                    $"Failed to initialise command {CommandName} with parameters {parameters.Trim()}!");
            string path = m.Value;
            Path = System.IO.Path.IsPathRooted(path) ? path : System.IO.Path.Combine(DragomanFX.FXPath, path);
            if (!File.Exists(Path)) throw new ArgumentException($"Failed to initialise command {CommandName}: no file found in {Path}.");
            LoadDescriptions();
        }

        public static string CommandName => "docsrc";
        public string Filename { get; private set; }
        public string Path { get; }

        private void LoadDescriptions()
        {
            // TODO: Implement description loading (XML)
        }

        public override string ToString() => $"{CommandName} {Filename} (at {Path})";
    }
}