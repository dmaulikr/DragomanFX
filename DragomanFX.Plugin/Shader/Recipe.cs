using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DragomanFX.Plugin.Math;
using DragomanFX.Plugin.Utils;

namespace DragomanFX.Plugin.Shader
{
    public class Recipe
    {
        private Dictionary<string, object> tagToValue;

        public Recipe(string path)
        {
            Name = Path.GetFileName(path);
            LoadRecipe(path);
        }

        private static Regex DecimalPattern { get; } = new Regex(@"^-?\d*\.\d+$");
        private static Regex FloatPattern { get; } = new Regex(@"^float\(\s*(-?(?:\d*\.\d+|\d+))\s*\)$");
        private static Regex HexPattern { get; } = new Regex(@"^0x\d+$");
        private static Regex IntPattern { get; } = new Regex(@"^-?\d+$");
        public string Name { get; }

        private static Regex Pattern { get; } =
            new Regex(@"#define\s+(?<name>[A-Za-z_][A-Za-z_0-9]*)\s+(?<value>\w+\([\d\.,\s]*\)|-?\w*\.?\w*)");

        private static Regex Vec2Pattern { get; } =
            new Regex(@"^float2\(\s*(-?(?:\d*\.\d+|\d+))\s*,\s*(-?(?:\d*\.\d+|\d+))\s*\)$");

        private static Regex Vec3Pattern { get; } =
            new Regex(@"^float3\(\s*(-?(?:\d*\.\d+|\d+))\s*,\s*(-?(?:\d*\.\d+|\d+))\s*,\s*(-?(?:\d*\.\d+|\d+))\s*\)$");

        private void LoadRecipe(string path)
        {
            string filePath = Path.IsPathRooted(path) ? path : Path.Combine(DragomanFX.FXPath, path);
            Logger.LogLine($"Attempting to load recipe {Name}: {filePath}");
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Failed to find the recipe file!", filePath);
            if (Path.GetExtension(filePath) != ".h")
                throw new FileNotFoundException("The file must have .h extension!", filePath);

            MatchCollection matches;
            try
            {
                using (TextReader tr = File.OpenText(filePath))
                {
                    matches = Pattern.Matches(tr.ReadToEnd());
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

            Logger.LogLine(LogLevel.Info, $"Recipe {Name} is loaded. Found lines of configurations: {matches.Count}.");

            tagToValue = new Dictionary<string, object>();
            foreach (Match match in matches)
            {
                ParseLine(match);
            }

            Logger.LogLine(LogLevel.Info, $"Parsing complete. Parsed {tagToValue.Count} values successfully.");
        }

        private void ParseLine(Match match)
        {
            string name = match.Groups["name"].Value;

            if (tagToValue.ContainsKey(name))
            {
                Logger.LogLine(LogLevel.Warning, $"Duplicate value for {name} found!");
                return;
            }

            string value = match.Groups["value"].Value;
            object val;
            try
            {
                if (HexPattern.IsMatch(value))
                    val = Convert.ToUInt32(value, 16);
                else if (IntPattern.IsMatch(value))
                    val = int.Parse(value, new NumberFormatInfo());
                else if (DecimalPattern.IsMatch(value))
                    val = float.Parse(value, new NumberFormatInfo());
                else
                {
                    Match parseMatch;
                    if ((parseMatch = FloatPattern.Match(value)).Success)
                        val = float.Parse(parseMatch.Groups[1].Value, new NumberFormatInfo());
                    else if ((parseMatch = Vec2Pattern.Match(value)).Success)
                        val = Vec2f.Parse(parseMatch.Groups[1].Value, parseMatch.Groups[2].Value);
                    else if ((parseMatch = Vec3Pattern.Match(value)).Success)
                    {
                        val = Vec3f.Parse(
                        parseMatch.Groups[1].Value,
                        parseMatch.Groups[2].Value,
                        parseMatch.Groups[3].Value);
                    }
                    else
                    {
                        Logger.LogLine(LogLevel.Warning, $"No valid parser found for {name}!");
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogLine(LogLevel.Warning, $"Failed to parse {name} {value}. Reason: {e.GetType()}: {e.Message}");
                return;
            }

            tagToValue.Add(name, val);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"RECIPE[{Name}]({tagToValue.Count} values):\n");
            foreach (KeyValuePair<string, object> value in tagToValue)
            {
                sb.AppendLine($"{value.Key} {value.Value}");
            }
            return sb.ToString();
        }
    }
}