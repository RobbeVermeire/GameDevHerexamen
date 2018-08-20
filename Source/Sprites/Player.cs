using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace PlatformGame.Source
{
    public class Player : Sprite
    {

        private KeyboardState _keyboardState;
        private float _speed = 10f;
        public bool IsColliding;

        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y,_animation.CurrentFrame.SourceRectangle.Width, _animation.CurrentFrame.SourceRectangle.Height);
            }
            set => base.CollisionRect = value;
        }

        public Player(Texture2D tex, Vector2 pos, SpriteBatch batch, bool isAnimated = false) : base(tex, pos, batch, isAnimated)
        {
            Acceleration = Constants.Gravity;

            _animation = new Animation();

            _animation.AddFrame(new Rectangle(0, 0, 72, 97));
            _animation.AddFrame(new Rectangle(73, 0, 72, 97));
            _animation.AddFrame(new Rectangle(146, 0, 72, 97));
            _animation.AddFrame(new Rectangle(0, 98, 72, 97));
            _animation.AddFrame(new Rectangle(73, 98, 72, 97));
            _animation.AddFrame(new Rectangle(146, 98, 72, 97));
            _animation.AddFrame(new Rectangle(219, 0, 72, 97));
            _animation.AddFrame(new Rectangle(292, 0, 72, 97));
            _animation.AddFrame(new Rectangle(219, 98, 72, 97));
            _animation.AddFrame(new Rectangle(365, 0, 72, 97));
            _animation.AddFrame(new Rectangle(292, 98, 72, 97));

            CollisionRect = new Rectangle((int)Position.X, (int)Position.Y, 72, 97);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            SimulateFriction();
            CheckCollisions(sprites);
            Position += Velocity;
            UpdateAnimation(gameTime);


            Velocity = Vector2.Zero;

        }

        private void UpdateAnimation(GameTime gameTime)
        {
            if (_keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.Right))
                _animation.Update(gameTime);

        }

        private void Move()
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Left))
                Velocity.X = -_speed;
            else if (_keyboardState.IsKeyDown(Keys.Right))
                Velocity.X = _speed;
            if (_keyboardState.IsKeyDown(Keys.Up))
                Velocity.Y = -_speed;
            else if (_keyboardState.IsKeyDown(Keys.Down))
                Velocity.Y = _speed;
        }
        /// <summary>
        /// Helper functies voor betere code:
        /// </summary>
        
        private void CheckCollisions(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                    (this.Velocity.X < 0 & this.IsTouchingRight(sprite)))
                {
                    this.Velocity.X = 0;
                }
                    

                if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                    (this.Velocity.Y < 0 & this.IsTouchingBottom(sprite)))
                {
                    this.Velocity.Y = 0;
                }
                    
            }
        }

        private void SimulateFriction()
        {
            Velocity -= Velocity * Vector2.One * .2f;
        }
    }
}
