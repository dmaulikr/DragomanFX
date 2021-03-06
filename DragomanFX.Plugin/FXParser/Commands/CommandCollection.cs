﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Commands
{
    public static class CommandCollection
    {
        private static readonly Regex pattern = new Regex(@"#(?<command>\w+)\s+(?<parameters>.+)");
        private static readonly Dictionary<string, Type> commands = new Dictionary<string, Type>();

        static CommandCollection()
        {
            foreach (Type type in
                typeof (Command).Assembly.GetTypes()
                                .Where((type => type != typeof (Command) && typeof (Command).IsAssignableFrom(type))))
            {
                string command = (string) type.GetProperty("CommandName").GetValue(null, null);

                commands.Add(command, type);
            }
        }

        public static Command CreateCommand(Recipe recipe, string command)
        {
            command = command.Trim();
            Match match;
            if (!(match = pattern.Match(command)).Success) return null;

            Command result = null;
            foreach (
                KeyValuePair<string, Type> type in commands.Where(type => match.Groups["command"].Value == type.Key))
            {
                result = (Command) Activator.CreateInstance(type.Value, match.Groups["parameters"].Value, recipe);
            }

            return result;
        }
    }
}