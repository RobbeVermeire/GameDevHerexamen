using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source.Sprites
{
    public class Enemy : Sprite
    {
        public Enemy(Texture2D tex, Vector2 pos, SpriteBatch batch,Dictionary<string,Animation> animations) : base(tex, pos, batch,animations)
        {

        }
        public override void Touches(Sprite sprite, Direction? touchedSide = null)
        {
            if(sprite is Player)
            {
                Player player = sprite as Player;
                player.Hit(this, 1);
            }
        }
    }
}
