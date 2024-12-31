using System.Collections.Generic;
using HelmareLabs.Rainbow.Graphics.Internal;
using HelmareLabs.Rainbow.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelmareLabs.Rainbow.Graphics
{
    public class RenderContext(string name)
    {
        private readonly Queue<IDrawCall> _drawCalls = [];

        /// <summary>
        ///     Gets the name of the context.
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        ///     Gets or sets the transform matrix passed to the SpriteBatch.
        /// </summary>
        public Matrix? TransformMatrix { get; set; } = null;

        /// <summary>
        ///     <para>Gets or sets the minimum depth.</para>
        ///     <em>Applies a depth test to all draw calls when flushed.</em>
        /// </summary>
        public float MinimumDepth { get; set; } = 0;

        /// <summary>
        ///     Enqueues a draw call.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="nOrigin"></param>
        /// <param name="scale"></param>
        /// <param name="effects"></param>
        /// <param name="layerDepth"></param>
        public void Draw(
            Texture2D texture,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 nOrigin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth
        )
        {
            _drawCalls.Enqueue(
                new TextureDrawCall(
                    texture,
                    position,
                    color,
                    rotation,
                    nOrigin,
                    scale,
                    effects,
                    layerDepth
                )
            );
        }

        /// <summary>
        ///     Enqueues a draw call.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void Draw(Texture2D texture, Vector2 position, Color color) =>
            Draw(texture, position, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

        /// <summary>
        ///     Enqueues a draw call to the current context.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="transform"></param>
        /// <param name="color"></param>
        /// <param name="effects"></param>
        public void Draw(
            Texture2D texture,
            Transform2D transform,
            Color color,
            SpriteEffects effects = SpriteEffects.None
        ) =>
            Draw(
                texture,
                transform.Position,
                color,
                transform.Rotation,
                transform.Origin,
                transform.Scale,
                effects,
                transform.Z
            );

        /// <summary>
        ///     Enqueues a string draw call.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="nOrigin"></param>
        /// <param name="scale"></param>
        /// <param name="effects"></param>
        /// <param name="layerDepth"></param>
        public void DrawString(
            SpriteFont font,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 nOrigin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth
        )
        {
            _drawCalls.Enqueue(
                new StringDrawCall(
                    font,
                    text,
                    position,
                    color,
                    rotation,
                    nOrigin,
                    scale,
                    effects,
                    layerDepth
                )
            );
        }

        /// <summary>
        ///     Enqueues a string draw call.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        public void DrawString(
            SpriteFont font,
            string text,
            Vector2 position,
            Color color,
            float scale = 1
        ) =>
            DrawString(
                font,
                text,
                position,
                color,
                0,
                Vector2.Zero,
                Vector2.One * scale,
                SpriteEffects.None,
                0
            );

        /// <summary>
        ///     Enqueues a string draw call.
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="transform"></param>
        /// <param name="color"></param>
        /// <param name="effects"></param>
        public void DrawString(
            SpriteFont font,
            string text,
            Transform2D transform,
            Color color,
            SpriteEffects effects = SpriteEffects.None
        ) =>
            DrawString(
                font,
                text,
                transform.Position,
                color,
                transform.Rotation,
                transform.Origin,
                transform.Scale,
                effects,
                transform.Z
            );

        /// <summary>
        ///     Dequeues all draw calls and renders them.
        /// </summary>
        /// <param name="transformMatrix">The transform matrix passed to the SpriteBatch.</param>
        /// <param name="minDepth">Applies a depth test to all draw calls if not null.</param>
        public void Flush(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, TransformMatrix);
            while (_drawCalls.Count > 0)
            {
                _drawCalls.Dequeue().Draw(batch, MinimumDepth);
            }
            batch.End();
        }
    }
}
