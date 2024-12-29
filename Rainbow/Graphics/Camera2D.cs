using HelmareLabs.Rainbow.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelmareLabs.Rainbow.Graphics
{
    /// <summary>
    ///     Projects and unprojects a virtual resolution onto a viewport.
    /// </summary>
    public class Camera2D
    {
        /// <summary>
        ///     <para>Gets the transform matrix.</para>
        ///     <em>Note: This will update after `UpdateMatrix()` is called</em>
        /// </summary>
        public Matrix TransformMatrix { get; private set; }

        /// <summary>
        ///     <para>Gets the inverted transform matrix.</para>
        ///     <em>Note: This will update after `UpdateMatrix()` is called</em>
        /// </summary>
        public Matrix InvertedMatrix { get; private set; }

        /// <summary>
        ///     <para>Gets the virtrual width.</para>
        ///     <em>Note: This will update after `UpdateMatrix()` is called</em>
        /// </summary>
        public float VirtualWidth { get; private set; } = 0;

        /// <summary>
        ///     Gets or sets the virtual height (<em>double the orthographic size</em>).
        /// </summary>
        public float VirtualHeight
        {
            get => OrthographicSize * 2;
            set => OrthographicSize = value / 2;
        }

        /// <summary>
        ///     Gets or sets the orthographic size.
        /// </summary>
        public float OrthographicSize { get; set; }

        /// <summary>
        ///     <para>Gets or sets whether the center should be the origin.</para>
        ///     <em>Note: This is true by default</em>
        /// </summary>
        public bool CenterOrigin { get; set; } = true;

        /// <summary>
        ///     <para>Gets the transform.</para>
        ///     <em>Note: `Transform.Scale` does not affect the camera.</em>
        /// </summary>
        public Transform2D Transform { get; } = new Transform2D();

        /// <summary>
        ///     Updates the transform matrix based on the given viewport.
        /// </summary>
        /// <param name="viewport"></param>
        public void UpdateMatrix(Viewport viewport)
        {
            // Set virtual width.
            VirtualWidth = (float)viewport.Width / viewport.Height * VirtualHeight;

            // Create initial viewport.
            Matrix m = Matrix.CreateScale(viewport.Height / VirtualHeight);

            // Center the origin.
            if (CenterOrigin)
            {
                m = Matrix.Multiply(
                    m,
                    Matrix.CreateTranslation(viewport.Width / 2, viewport.Height / 2, 0)
                );
            }

            // Translate camera in world space.
            Vector2 pos = Transform.AbsolutePosition;
            m = Matrix.Multiply(Matrix.CreateTranslation(-pos.X, -pos.Y, 0), m);

            // Rotates the camera.
            m = Matrix.Multiply(Matrix.CreateRotationZ(Transform.AbsoluteRotation), m);

            TransformMatrix = m;
            InvertedMatrix = Matrix.Invert(m);
        }

        /// <summary>
        ///     Transforms a screen space vector to a world space vector.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 ScreenToWorld(Vector2 position)
        {
            return Vector2.Transform(position, InvertedMatrix);
        }

        /// <summary>
        ///     Transforms a screen space vector to a world space vector.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 ScreenToWorld(Point position) => ScreenToWorld(position.ToVector2());

        /// <summary>
        ///     Transforms a world space vector to a screen space vector.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 WorldToScreen(Vector2 position)
        {
            return Vector2.Transform(position, TransformMatrix);
        }
    }
}
