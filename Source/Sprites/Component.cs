using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source
{
    public abstract class Component
    {
        public abstract SpriteBatch SpriteBatch { get; set; }
        public abstract void Draw();
        public abstract void Update(GameTime gameTime);
    }
}
