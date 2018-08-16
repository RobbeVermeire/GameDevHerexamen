using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Source
{
    public class Sprite
    {
        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Texture2D Texture { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        public Rectangle CollisionRect;
        public Sprite(Texture2D tex, Vector2 pos, SpriteBatch batch)
        {
            Texture = tex;
            Position = pos;
            SpriteBatch = batch;
            CollisionRect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public virtual void Draw()
        {
            SpriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
