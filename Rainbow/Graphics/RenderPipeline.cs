using System;
using System.Collections.Generic;
using HelmareLabs.Rainbow.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelmareLabs.Rainbow.Graphics
{
    public class RenderPipeline(GraphicsDevice device)
    {
        private readonly List<RenderContext> _contexts = [];

        public SpriteBatch SpriteBatch { get; } = new SpriteBatch(device);

        /// <summary>
        ///     Gets the current context.
        /// </summary>
        public RenderContext Context => _contexts[ContextID];

        /// <summary>
        ///     Gets or sets the current context by ID.
        /// </summary>
        public int ContextID { get; set; } = -1;

        /// <summary>
        ///     Adds a render context to the pipeline.
        /// </summary>
        /// <param name="ctx"></param>
        public void AddContext(string name) => _contexts.Add(new RenderContext(name));

        /// <summary>
        ///     Sets the current context to the named context.
        /// </summary>
        /// <param name="name"></param>
        public void SetContext(string name)
        {
            for (int i = 0; i < _contexts.Count; i++)
            {
                if (_contexts[i].Name.ToLowerInvariant() == name.ToLowerInvariant())
                {
                    ContextID = i;
                    return;
                }
            }
            throw new ArgumentOutOfRangeException(nameof(name));
        }

        /// <summary>
        ///     Enqueues a draw call to the current context.
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
        ) => Context.Draw(texture, position, color, rotation, nOrigin, scale, effects, layerDepth);

        /// <summary>
        ///     Enqueues a draw call to the current context.
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
        ///     Enqueues a string draw call to the current context.
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
        ) =>
            Context.DrawString(
                font,
                text,
                position,
                color,
                rotation,
                nOrigin,
                scale,
                effects,
                layerDepth
            );

        /// <summary>
        ///     Enqueues a string draw call to the current context.
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
        ///     Flushes all contexts.
        /// </summary>
        public void FlushAll()
        {
            foreach (RenderContext ctx in _contexts)
            {
                ctx.Flush(SpriteBatch);
            }
        }
    }
}
