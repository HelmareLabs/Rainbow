using HelmareLabs.Rainbow.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelmareLabs.Rainbow.Graphics.Internal
{
    internal sealed class StringDrawCall(
        SpriteFont font,
        string text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 nOrigin,
        Vector2 scale,
        SpriteEffects effects,
        float layerDepth
    ) : IDrawCall
    {
        public void Draw(SpriteBatch batch, float? minDepth)
        {
            if (layerDepth >= minDepth)
            {
                Vector2 size = font.MeasureString(text);
                Vector2 origin = new Vector2 { X = nOrigin.X * size.X, Y = nOrigin.Y * size.Y };

                batch.DrawString(
                    font,
                    text,
                    position,
                    color,
                    rotation,
                    origin,
                    scale,
                    effects,
                    layerDepth
                );
            }
        }
    }
}
