using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PlatformGame.Source
{
    public class Sprite : Component
    {
        protected Animation _animation;
        public Vector2 Velocity;
        public Vector2 Acceleration;

        public bool IsAnimated { get; set; }
        public Vector2 Position;
        public Texture2D Texture { get; set; }
        public override SpriteBatch SpriteBatch { get; set; }

        public virtual Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
        public Sprite(Texture2D tex, Vector2 pos, SpriteBatch batch, bool isAnimated = false)
        {
            Texture = tex;
            Position = pos;
            SpriteBatch = batch;
            IsAnimated = isAnimated;
        }

        public override void Draw()
        {
            if (_animation != null)
                SpriteBatch.Draw(Texture, Position, _animation.CurrentFrame.SourceRectangle, Color.White);
            else SpriteBatch.Draw(Texture, Position, Color.White);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites )
        {

        }



        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.CollisionRect.Right + this.Velocity.X > sprite.CollisionRect.Left &&
              this.CollisionRect.Left < sprite.CollisionRect.Left &&
              this.CollisionRect.Bottom > sprite.CollisionRect.Top &&
              this.CollisionRect.Top < sprite.CollisionRect.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.CollisionRect.Left + this.Velocity.X < sprite.CollisionRect.Right &&
              this.CollisionRect.Right > sprite.CollisionRect.Right &&
              this.CollisionRect.Bottom > sprite.CollisionRect.Top &&
              this.CollisionRect.Top < sprite.CollisionRect.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.CollisionRect.Bottom + this.Velocity.Y > sprite.CollisionRect.Top &&
              this.CollisionRect.Top < sprite.CollisionRect.Top &&
              this.CollisionRect.Right > sprite.CollisionRect.Left &&
              this.CollisionRect.Left < sprite.CollisionRect.Right;
              
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.CollisionRect.Top + this.Velocity.Y < sprite.CollisionRect.Bottom &&
              this.CollisionRect.Bottom > sprite.CollisionRect.Bottom &&
              this.CollisionRect.Right > sprite.CollisionRect.Left &&
              this.CollisionRect.Left < sprite.CollisionRect.Right;
        }
    }
}
