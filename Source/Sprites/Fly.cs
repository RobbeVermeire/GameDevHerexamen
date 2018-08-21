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
        public Fly(Texture2D tex, Vector2 pos, SpriteBatch batch, Dictionary<string, Animation> animations) : base(tex, pos, batch, animations)
        {
            Velocity = new Vector2(1f, 0);
            MaxDistance = 20f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if(TraveledDistance >= MaxDistance)
            {
                Velocity = -Velocity;   
            }

            Position += Velocity;
            TraveledDistance += Velocity.X;
            base.Update(gameTime, sprites);
        }
    }
}
