using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelmareLabs.Rainbow.Graphics.Internal
{
    internal sealed class TextureDrawCall(
        Texture2D texture,
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
                Vector2 origin = new Vector2
                {
                    X = nOrigin.X * texture.Bounds.Width,
                    Y = nOrigin.Y * texture.Bounds.Height,
                };

                batch.Draw(
                    texture,
                    position,
                    null,
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
