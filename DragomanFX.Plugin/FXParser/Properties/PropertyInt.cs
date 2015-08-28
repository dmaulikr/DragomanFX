using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public class PropertyInt : Property
    {
        public static Regex Pattern { get; } = new Regex(@"^-?\d+$");

        public int Value { get; set; }

        public PropertyInt(Match match) : base(match)
        {
            Value = int.Parse(match.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
