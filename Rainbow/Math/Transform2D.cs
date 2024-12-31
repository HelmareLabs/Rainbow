using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace HelmareLabs.Rainbow.Math
{
    /// <summary>
    ///     Holds position, rotation, and scale information about an object.
    /// </summary>
    public class Transform2D
    {
        /// <summary>
        ///     Gets or sets the relative position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Gets or sets the Z position.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        ///     Gets or sets the 3D position.
        /// </summary>
        public Vector3 Position3
        {
            get => new Vector3(Position, Z);
            set
            {
                Position = new Vector2(value.X, value.Y);
                Z = value.Z;
            }
        }

        /// <summary>
        ///     Gets or sets the relative rotation.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        ///     Gets or sets the relative scale of an object.
        /// </summary>
        public Vector2 Scale { get; set; } = Vector2.One;

        /// <summary>
        ///     Gets or sets a normalized origin.
        /// </summary>
        public Vector2 Origin { get; set; } = Vector2.Zero;
    }
}
