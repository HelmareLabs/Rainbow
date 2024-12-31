using Microsoft.Xna.Framework.Graphics;

namespace HelmareLabs.Rainbow.Graphics.Internal
{
    /// <summary>
    ///     A function that draws to a SpriteBatch.
    /// </summary>
    internal interface IDrawCall
    {
        public void Draw(SpriteBatch batch, float? minDepth);
    }
}
