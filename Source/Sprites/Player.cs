using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Source.Managers;
using System;
using System.Collections.Generic;

namespace PlatformGame.Source
{
    public class Player : Sprite
    {
        private KeyboardState _keyboardState;
        private readonly float _speed = 10f;
        private bool jumping;

        public Player(Texture2D tex, Vector2 pos, SpriteBatch batch, Dictionary<string,Animation> animations) : base(tex, pos, batch,animations)
        {
            jumping = true;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            CheckCollisions(sprites);
            Position += Velocity;
            Velocity.X = 0;
            Velocity.Y++;
            UpdateAnimation(gameTime);
            Console.WriteLine(Position);
            base.Update(gameTime, sprites);
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            if (jumping)
                _animationManager.Play(_animations["Jump"]);
            else if (_keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.Right))
                _animationManager.Play(_animations["WalkRight"]);
            else
                _animationManager.Play(_animations["Stand"]);
        }

        private void Move()
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Left))
                Velocity.X = -_speed;
            else if (_keyboardState.IsKeyDown(Keys.Right))
                Velocity.X = _speed;
            if (_keyboardState.IsKeyDown(Keys.Space) && !jumping)
            {
                Velocity.Y = -14f;
                jumping = true;
            }
        }
        /// <summary>
        /// Helper functies voor betere code:
        /// </summary>
        
        private void CheckCollisions(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if(sprite is Tile)
                {

                    if(this.Velocity.Y > 0 && this.IsTouchingTop(sprite))
                    {
                        jumping = false;
                    }

                    if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                        (this.Velocity.X < 0 && this.IsTouchingRight(sprite)))
                    {
                        this.Velocity.X = 0;
                    }


                    if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                        (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)))
                    {
                        this.Velocity.Y = 0;
                    }
                }


            }
        }
    }
}
