using System.Net;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;

namespace HelmareLabs.Rainbow.Math
{
    public static class VectorExt
    {
        /// <summary>
        ///     Removes the Z value.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        /// <summary>
        ///     Converts this point to a Vector2.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}
