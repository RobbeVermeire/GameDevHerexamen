using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Source.Enemies
{
    class Blocker : Enemy
    {
        private float _MaxTime = 15f;
        private float _currentTime;
        private bool _collided;

        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 51,51);
            }
        }

        public Blocker(Texture2D tex, Vector2 pos, SpriteBatch batch, Dictionary<string, Animation> animations) : base(tex, pos, batch, animations)
        {
            _currentTime = 0f;
        }

        public override void Touches(Sprite sprite, Direction? touchedSide = null)
        {
            base.Touches(sprite, touchedSide);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_currentTime >= _MaxTime)
            {
                Console.WriteLine("TIMBER");
                _currentTime = 0f;
                Velocity.Y = -5f;
                _animationManager.Play(_animations["Down"]);
            }
            if(Velocity.Y != 0)
                CheckCollisions(sprites);
            if (_collided)
            {
                _collided = false;
                Velocity.Y = 4f;
                _animationManager.Play(_animations["Up"]);
            }

                
            
        }

        private void CheckCollisions(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (this.Velocity.Y > 0 && this.IsTouchingTop(sprite))
                {
                    sprite.Touches(this, Direction.Top);
                    _collided = true;
                }
            }
        }
    }
}
