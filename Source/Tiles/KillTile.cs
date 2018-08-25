using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source.Sprites
{
    class KillTile : Tile
    {
        public KillTile(Texture2D TileTexture, Vector2 tilePosition, SpriteBatch spriteBatch, bool isBlocked) : base(TileTexture, tilePosition, spriteBatch, isBlocked)
        {
        }
        public override void Touches(Sprite sprite, Direction? touchedSide = null)
        {
            Player player = sprite as Player;
            player.Respawn(100, 600);
            base.Touches(sprite, touchedSide);
        }
    }
}
