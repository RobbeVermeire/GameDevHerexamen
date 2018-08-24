using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Source.Managers;
using PlatformGame.Source.Sprites;
using System;
using System.Collections.Generic;

namespace PlatformGame.Source
{
    public class Player : Sprite
    {
        private KeyboardState _keyboardState;
        private readonly float _speed = 10f;
        private bool _jumping;
        private bool _isInvincible;
        public readonly int MaxHealth = 6;

        public int Health { get; set; }
        public int Coins { get; set; }

        public Player(Texture2D tex, Vector2 pos, SpriteBatch batch, Dictionary<string,Animation> animations) : base(tex, pos, batch,animations)
        {
            _jumping = true;
            CollisionRectOffset = new Rectangle(0, 0, 10, 3);
            Health = 6;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            CheckCollisions(gameTime,sprites);
            UpdateAnimation(gameTime);

            Position += Velocity;
            if (Velocity.X > 0)
                Velocity.X -= 1f;
            if (Velocity.X < 0)
                Velocity.X += 1f;
            Velocity.Y++;

            //Console.WriteLine(Velocity.X);
            base.Update(gameTime, sprites);
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            if (!_isInvincible)
            {
                if (_jumping && Velocity.X < 0)
                    _animationManager.Play(_animations["JumpLeft"]);
                else if (_jumping)
                    _animationManager.Play(_animations["JumpRight"]);
                else if (Velocity.X > 0)
                    _animationManager.Play(_animations["WalkRight"]);
                else if (Velocity.X < 0)
                    _animationManager.Play(_animations["WalkLeft"]);
                else
                    _animationManager.Play(_animations["StandRight"]);
            }
            else if(Velocity.X < 0)
                _animationManager.Play(_animations["HurtLeft"]);
            else if(Velocity.X > 0)
                _animationManager.Play(_animations["HurtRight"]);
        }

        private void Move()
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Left))
                Velocity.X = -_speed;
            else if (_keyboardState.IsKeyDown(Keys.Right))
                Velocity.X = _speed;
            if (_keyboardState.IsKeyDown(Keys.Space) && !_jumping)
            {
                Velocity.Y = -14f;
                _jumping = true;
            }
        }
        /// <summary>
        /// Helper functies voor betere code:
        /// </summary>
        
        private void CheckCollisions(GameTime gametime, List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if(sprite is Tile)
                {

                    if(this.Velocity.Y > 0 && this.IsTouchingTop(sprite))
                    {
                        _jumping = false;
                        if(_isInvincible)
                        {
                            _isInvincible = false;
                            _animationManager.DrawColor = Color.White;
                        }
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

                if (sprite is Enemy)
                {
                    if(!_isInvincible)
                    {
                       if (this.IsTouchingBottom(sprite) ||
                       this.IsTouchingLeft(sprite) ||
                       this.IsTouchingRight(sprite))
                        {
                            Health--;
                            if (Health == 0)
                            {
                                Respawn(100, 600);
                                return;
                            }
                                
                            JumpAwayFrom(sprite);
                            _isInvincible = true;
                            _animationManager.DrawColor = new Color(226, 101, 80);
                            Console.WriteLine("OUCH");

                        }
                    }               
                }


            }
        }

        private void Respawn(int x, int y)
        {
            Health = 6;
            Coins = 0;
            Velocity = Vector2.Zero;
            _animationManager.Play(_animations["StandRight"]);
            Position = new Vector2(x, y);
        }

        private void JumpAwayFrom(Sprite sprite)
        {
            Velocity = Vector2.Zero;
           if(sprite.Position.X > this.Position.X)
            {
                Velocity += new Vector2(-5f, -15f);
            }
            if (sprite.Position.X < this.Position.X)
            {
                Velocity += new Vector2(5f, -15f);
            }
        }
    }
}
