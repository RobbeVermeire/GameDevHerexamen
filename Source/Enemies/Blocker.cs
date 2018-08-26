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
    public class Blocker : Enemy
    {
        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 51, 51);
            }
        }
        public Blocker(Texture2D tex, Vector2 pos, SpriteBatch batch, Dictionary<string, Animation> animations) : base(tex, pos, batch, animations)
        {
        }
    }
}
