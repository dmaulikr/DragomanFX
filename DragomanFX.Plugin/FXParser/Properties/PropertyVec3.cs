using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public class PropertyVec3 : Property
    {
        public static Regex Pattern { get; } = new Regex(@"^float3\(\s*(-?(?:\d*\.\d+|\d+))\s*,\s*(-?(?:\d*\.\d+|\d+))\s*,\s*(-?(?:\d*\.\d+|\d+))\s*\)$");

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public PropertyVec3(Match match) : base(match)
        {
            X = float.Parse(match.Groups[1].Value, new NumberFormatInfo());
            Y = float.Parse(match.Groups[2].Value, new NumberFormatInfo());
            Z = float.Parse(match.Groups[3].Value, new NumberFormatInfo());
        }

        public override string ToString() => $"float3({X.ToString("0.0###", new NumberFormatInfo())}, {Y.ToString("0.0###", new NumberFormatInfo())}, {Z.ToString("0.0###", new NumberFormatInfo())})";
    }
}
