using System;
using System.Text.RegularExpressions;
using DragomanFX.Plugin.Utils;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public class PropertyHex : Property
    {
        private uint _val;

        public PropertyHex(string name, Match match) : base(name, match)
        {
            Value = Convert.ToUInt32(match.Value, 16);
        }

        public static Regex Pattern { get; } = new Regex(@"^0x\d+$");

        public uint Value
        {
            get { return _val; }
            set { _val = MathHelper.Clamp(value, Min, Max); }
        }

        public uint Min { get; private set; } = uint.MinValue;
        public uint Max { get; private set; } = uint.MaxValue;

        public override void ParseMinMax(string min, string max)
        {
            Min = Convert.ToUInt32(min, 16);
            Max = Convert.ToUInt32(max, 16);
        }

        public override string ToString() => $"0x{Convert.ToString(Value, 16)}";
    }
}