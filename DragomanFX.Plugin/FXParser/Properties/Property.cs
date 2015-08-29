using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public abstract class Property
    {
        protected Property(string name, Match match)
        {
            Name = name;
        }

        public string Name { get; }
        public abstract void ParseMinMax(string min, string max);
        public abstract override string ToString();
    }
}