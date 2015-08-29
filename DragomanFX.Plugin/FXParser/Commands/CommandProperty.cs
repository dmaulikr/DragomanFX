using System;
using System.Text.RegularExpressions;
using DragomanFX.Plugin.FXParser.Properties;

namespace DragomanFX.Plugin.FXParser.Commands
{
    public class CommandProperty : Command
    {
        private static readonly Regex pattern =
            new Regex(@"(?<name>[A-Za-z_][A-Za-z_0-9]*)\s+(?<value>\w+\([\d\.,\s]*\)|-?\w*\.?\w*)");

        public CommandProperty(string parameters, Recipe recipe) : base(parameters, recipe)
        {
            Match match = pattern.Match(parameters);
            if (!match.Success)
                throw new ArgumentException(
                    $"Failed to initialise command {CommandName} with parameters {parameters.Trim()}!");
            Property = PropertyCollection.GetProperty(match.Groups["name"].Value, match.Groups["value"].Value);
            if (Property == null) throw new ArgumentException($"Failed to initialise command {CommandName}'s property!");
            Name = match.Groups["name"].Value;
        }

        public static string CommandName => "define";
        public string Name { get; }
        public Property Property { get; }
        public override string ToString() => $"#{CommandName} {Name} {Property}";
    }
}