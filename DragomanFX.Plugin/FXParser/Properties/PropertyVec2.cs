using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public class PropertyVec2 : Property
    {
        public static Regex Pattern { get; } = new Regex(@"^float2\(\s*(-?(?:\d*\.\d+|\d+))\s*,\s*(-?(?:\d*\.\d+|\d+))\s*\)$");

        public float X { get; set; }
        public float Y { get; set; }

        public PropertyVec2(Match match) : base(match)
        {
            X = float.Parse(match.Groups[1].Value, new NumberFormatInfo());
            Y = float.Parse(match.Groups[2].Value, new NumberFormatInfo());
        }
        public override string ToString() => $"float2({X.ToString("0.0###", new NumberFormatInfo())}, {Y.ToString("0.0###", new NumberFormatInfo())})";
    }
}
