using System.Globalization;
using System.Text.RegularExpressions;
using DragomanFX.Plugin.Utils;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public class PropertyDecimal : Property
    {
        private float _val;

        public PropertyDecimal(string name, Match match) : base(name, match)
        {
            Value = float.Parse(match.Value, new NumberFormatInfo());
        }

        public static Regex Pattern { get; } = new Regex(@"^-?\d*\.\d+$");

        public float Value
        {
            get { return _val; }
            set { _val = MathHelper.Clamp(value, Min, Max); }
        }

        public float Min { get; private set; } = float.MinValue;
        public float Max { get; private set; } = float.MaxValue;

        public override void ParseMinMax(string min, string max)
        {
            Min = float.Parse(min, new NumberFormatInfo());
            Max = float.Parse(max, new NumberFormatInfo());
        }

        public override string ToString() => Value.ToString("0.0###", new NumberFormatInfo());
    }
}