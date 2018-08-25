using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source.Sprites
{
    class Fly : Enemy
    {
        public float MaxDistance { get; set; }
        private float TraveledDistance { get; set; }
        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 72, 36);
            }
        }
        public Fly(Texture2D tex, Vector2 pos, SpriteBatch batch, Dictionary<string, Animation> animations) : base(tex, pos, batch, animations)
        {
            Velocity = new Vector2(1f, 0);
            MaxDistance = 140f;
            foreach (Animation animation in animations.Values)
            {
                animation.FrameSpeed = 0.1f;
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if(Math.Abs(TraveledDistance) >= Math.Abs(MaxDistance))
            {
                if (Velocity.X > 0)
                {
                    _animationManager.Play(_animations["Right"]);
                    Console.WriteLine("Right");
                }

                else if (Velocity.X < 0)
                {
                    _animationManager.Play(_animations["Left"]);
                    Console.WriteLine("Left");
                }
                Velocity = -Velocity;

            }

            Position += Velocity;
            TraveledDistance += Velocity.X;
            base.Update(gameTime, sprites);
        }
    }
}
