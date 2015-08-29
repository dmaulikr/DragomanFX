using System.Text.RegularExpressions;
using DragomanFX.Plugin.Utils;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public class PropertyInt : Property
    {
        private int _val;

        public PropertyInt(string name, Match match) : base(name, match)
        {
            Value = int.Parse(match.Value);
        }

        public static Regex Pattern { get; } = new Regex(@"^-?\d+$");

        public int Value
        {
            get { return _val; }
            set { _val = MathHelper.Clamp(value, Min, Max); }
        }

        public int Min { get; private set; } = int.MinValue;
        public int Max { get; private set; } = int.MaxValue;

        public override void ParseMinMax(string min, string max)
        {
            Min = int.Parse(min);
            Max = int.Parse(max);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}