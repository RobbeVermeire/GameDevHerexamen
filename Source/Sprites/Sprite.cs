using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Managers;
using System.Collections.Generic;
using System.Linq;

namespace PlatformGame.Source
{
    public class Sprite : Component
    {
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Vector2 _position;

        public Vector2 Velocity;
        public Vector2 Acceleration;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if(_animationManager != null)
                {
                    _animationManager.Position = _position;
                }
            }
        }
        public Texture2D Texture { get; set; }
        public override SpriteBatch SpriteBatch { get; set; }

        public virtual Rectangle CollisionRect
        {
            get
            {
                if (_animationManager == null)
                    return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

                else return new Rectangle((int)Position.X, (int)Position.Y, _animations.First().Value.FrameWidth-CollisionRectOffset.Width, _animations.First().Value.FrameHeight-CollisionRectOffset.Height);
            }
        }
        public virtual Rectangle CollisionRectOffset
        {
            get; set;
        }


        public Sprite(Texture2D tex, Vector2 pos, SpriteBatch batch)
        {
            Texture = tex;
            Position = pos;
            SpriteBatch = batch;
        }
        public Sprite(Texture2D tex, Vector2 pos, SpriteBatch batch, Dictionary<string,Animation> animations)
        {
            Texture = tex;
            Position = pos;
            SpriteBatch = batch;
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
            CollisionRectOffset = Rectangle.Empty;
        }

        public override void Draw()
        {
            if (_animationManager != null)
                _animationManager.Draw(SpriteBatch);

            else SpriteBatch.Draw(Texture, Position, Color.White);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites )
        {
            if(_animationManager != null)
            _animationManager.Update(gameTime);

        }

        public virtual void Touches(Sprite sprite, Direction? touchedSide = null)
        { }




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
