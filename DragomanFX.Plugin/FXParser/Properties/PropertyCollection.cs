using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public static class PropertyCollection
    {
        private static readonly Dictionary<Type, Regex> propertyTypes = new Dictionary<Type, Regex>();

        static PropertyCollection()
        {
            foreach (Type type in typeof (Property).Assembly.GetTypes())
            {
                if (type == typeof (Property) || !typeof (Property).IsAssignableFrom(type)) continue;
                Regex regex = (Regex) type.GetProperty("Pattern").GetValue(null, null);

                propertyTypes.Add(type, regex);
            }
        }

        public static Property GetProperty(string name, string value)
        {
            Property result = null;
            foreach (KeyValuePair<Type, Regex> propertyType in propertyTypes)
            {
                Match match;
                if ((match = propertyType.Value.Match(value)).Success)
                {
                    result = (Property) Activator.CreateInstance(propertyType.Key, name, match);
                }
            }

            return result;
        }
    }
}