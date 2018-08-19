using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Source
{
    public class Sprite : Component
    {
        protected Animation _animation;
        public bool IsAnimated { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Texture2D Texture { get; set; }
        public override SpriteBatch SpriteBatch { get; set; }

        public Rectangle CollisionRect;
        public Sprite(Texture2D tex, Vector2 pos, SpriteBatch batch, bool isAnimated=false)
        {
            Texture = tex;
            Position = pos;
            SpriteBatch = batch;
            IsAnimated = isAnimated;
            if (!isAnimated)
                CollisionRect = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            else;
                //Colisionrect in another constructor

        }

        public override void Draw()
        {
            if(_animation != null)
                SpriteBatch.Draw(Texture, Position, _animation.CurrentFrame.SourceRectangle, Color.White);
            else SpriteBatch.Draw(Texture, Position, Color.White);
        } 

        public override void Update(GameTime gameTime)
        {
          
        }
    }
}
