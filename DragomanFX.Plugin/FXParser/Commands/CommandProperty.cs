using System.Text.RegularExpressions;
using DragomanFX.Plugin.FXParser.Properties;

namespace DragomanFX.Plugin.FXParser.Commands
{
    public class CommandProperty : Command
    {
        private static readonly Regex pattern =
        new Regex(@"(?<name>[A-Za-z_][A-Za-z_0-9]*)\s+(?<value>\w+\([\d\.,\s]*\)|-?\w*\.?\w*)");

        public CommandProperty(Recipe recipe) : base(recipe) {}

        public CommandProperty(string name, Property property, Recipe recipe) : base(recipe)
        {
            Name = name;
            Property = property;
        }

        public static string CommandName => "define";
        public string Name { get; }
        public Property Property { get; }

        public override Command Parse(string parameters)
        {
            Match match = pattern.Match(parameters);
            if (!match.Success)
                return null;
            Property prop = PropertyCollection.GetProperty(match.Groups["value"].Value);
            return prop != null ? new CommandProperty(match.Groups["name"].Value, prop, recipe) : null;
        }

        public override string ToString() => $"#{CommandName} {Name} {Property}";
    }
}