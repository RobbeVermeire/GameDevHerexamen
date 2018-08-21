using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source.Sprites
{
    class Enemy : Sprite
    {
        public Enemy(Texture2D tex, Vector2 pos, SpriteBatch batch) : base(tex, pos, batch)
        {

        }

        public override Rectangle CollisionRect => base.CollisionRect;
    }
}
