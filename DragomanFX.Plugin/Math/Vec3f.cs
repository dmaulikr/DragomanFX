using System.Globalization;

namespace DragomanFX.Plugin.Math
{
    public struct Vec3f
    {
        public float X, Y, Z;

        public Vec3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vec3f Parse(string x, string y, string z)
        {
            return new Vec3f(
            float.Parse(x, new NumberFormatInfo()),
            float.Parse(y, new NumberFormatInfo()),
            float.Parse(z, new NumberFormatInfo()));
        }

        public override string ToString()
        {
            return $"VEC3F[{X}; {Y}; {Z}]";
        }
    }
}