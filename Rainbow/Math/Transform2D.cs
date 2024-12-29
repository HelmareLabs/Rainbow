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
        ///     Gets or sets a parent transform.
        /// </summary>
        public Transform2D? Parent { get; set; }

        /// <summary>
        ///     Gets or sets the relative position in space.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Gets or sets the absolute position in space.
        /// </summary>
        public Vector2 AbsolutePosition
        {
            get => (Position + Parent?.AbsolutePosition) ?? Position;
            set => Position = (value - Parent?.AbsolutePosition) ?? value;
        }

        /// <summary>
        ///     Gets or sets the relative depth of an object.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        ///     Gets or sets the absolute depth of an object.
        /// </summary>
        public float AbsoluteZ
        {
            get => (Z + Parent?.AbsoluteZ) ?? Z;
            set => Z = (value - Parent?.AbsoluteZ) ?? value;
        }

        /// <summary>
        ///     Gets or sets the relative rotation.
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        ///     Gets or sets the absolute rotation.
        /// </summary>
        public float AbsoluteRotation
        {
            get => (Rotation + Parent?.AbsoluteRotation) ?? Rotation;
            set => Rotation = (value - Parent?.AbsoluteRotation) ?? value;
        }

        /// <summary>
        ///     Gets or sets the relative scale of an object.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        ///     Gets or sets the absolute scale of an object.
        /// </summary>
        public Vector2 AbsoluteScale
        {
            get
            {
                if (Parent == null)
                {
                    return Scale;
                }
                else
                {
                    float x = Scale.X * Parent.AbsoluteScale.X;
                    float y = Scale.Y * Parent.AbsoluteScale.Y;
                    return new Vector2(x, y);
                }
            }
            set
            {
                if (Parent == null)
                {
                    Scale = value;
                }
                else
                {
                    float x = value.X / Parent.AbsoluteScale.X;
                    float y = value.Y / Parent.AbsoluteScale.Y;
                    Scale = new Vector2(x, y);
                }
            }
        }

        public Transform2D()
        {
            Scale = Vector2.One;
        }
    }
}
