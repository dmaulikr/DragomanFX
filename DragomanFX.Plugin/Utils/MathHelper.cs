using System;

namespace DragomanFX.Plugin.Utils
{
    public class MathHelper
    {
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            return val.CompareTo(min) < 0 ? min : (val.CompareTo(max) > 0 ? max : val);
        }
    }
}