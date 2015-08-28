using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public class PropertyHex : Property
    {
        public static Regex Pattern { get; } = new Regex(@"^0x\d+$");

        public uint Value { get; set; }

        public PropertyHex(Match match) : base(match)
        {
            Value = Convert.ToUInt32(match.Value, 16);
        }
        public override string ToString() => $"0x{Convert.ToString(Value, 16)}";
    }
}
