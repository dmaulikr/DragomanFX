using System.Globalization;

namespace DragomanFX.Plugin.Math
{
    public struct Vec2f
    {
        public float X, Y;

        public Vec2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vec2f Parse(string x, string y)
        {
            return new Vec2f(float.Parse(x, new NumberFormatInfo()), float.Parse(y, new NumberFormatInfo()));
        }

        public override string ToString()
        {
            return $"VEC2F[{X}; {Y}]";
        }
    }
}