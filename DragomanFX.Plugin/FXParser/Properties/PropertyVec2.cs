using System;
using System.Globalization;
using System.Text.RegularExpressions;
using DragomanFX.Plugin.Utils;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public class PropertyVec2 : Property
    {
        private float _x, _y;

        public PropertyVec2(string name, Match match) : base(name, match)
        {
            X = float.Parse(match.Groups[1].Value, new NumberFormatInfo());
            Y = float.Parse(match.Groups[2].Value, new NumberFormatInfo());
        }

        public static Regex Pattern { get; } =
            new Regex(@"^float2\(\s*(-?(?:\d*\.\d+|\d+))\s*,\s*(-?(?:\d*\.\d+|\d+))\s*\)$");

        public float X
        {
            get { return _x; }
            set { _x = MathHelper.Clamp(value, XMin, XMax); }
        }

        public float Y
        {
            get { return _y; }
            set { _y = MathHelper.Clamp(value, YMin, YMax); }
        }

        public float XMin { get; set; } = float.MinValue;
        public float YMin { get; set; } = float.MinValue;
        public float XMax { get; set; } = float.MaxValue;
        public float YMax { get; set; } = float.MaxValue;

        public override void ParseMinMax(string min, string max)
        {
            Match minMatch = Pattern.Match(min);
            Match maxMatch = Pattern.Match(max);
            if (!minMatch.Success || !maxMatch.Success) throw new ArgumentException("Failed to parse Vec2 max and min values!");
            XMin = float.Parse(minMatch.Groups[1].Value, new NumberFormatInfo());
            YMin = float.Parse(minMatch.Groups[2].Value, new NumberFormatInfo());

            XMax = float.Parse(maxMatch.Groups[1].Value, new NumberFormatInfo());
            YMax = float.Parse(maxMatch.Groups[2].Value, new NumberFormatInfo());
        }

        public override string ToString()
            => $"float2({X.ToString("0.0###", new NumberFormatInfo())}, {Y.ToString("0.0###", new NumberFormatInfo())})";
    }
}