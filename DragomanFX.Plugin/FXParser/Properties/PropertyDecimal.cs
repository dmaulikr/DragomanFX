using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public class PropertyDecimal : Property
    {
        public static Regex Pattern { get; } = new Regex(@"^-?\d*\.\d+$");

        public float Value { get; set; }

        public PropertyDecimal(Match match) : base(match)
        {
            Value = float.Parse(match.Value, new NumberFormatInfo());
        }

        public override string ToString() => Value.ToString("0.0###", new NumberFormatInfo());
    }
}
