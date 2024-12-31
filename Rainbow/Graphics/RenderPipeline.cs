using System;
using System.Collections.Generic;
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
