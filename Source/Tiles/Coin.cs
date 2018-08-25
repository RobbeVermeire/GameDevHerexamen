using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source.Tiles
{
    public class Coin : Tile,IPickup 
    {
        private bool _wasPickedUp { get; set; }
        public Coin(Texture2D Texture, Vector2 tilePosition, SpriteBatch spriteBatch) : base(Texture, tilePosition, spriteBatch, true)
        {

        }

        public void Pickup(Player player)
        {
            if(!_wasPickedUp)
            {
                player.Coins++;
                _wasPickedUp = true;
            }           
        }
        public override void Draw()
        {
            if(!_wasPickedUp)
            {
               base.Draw();
            }

        }
        public override void Touches(Sprite sprite, Direction? touchedSide = null)
        {
            Player player = sprite as Player;

            Pickup(player);
        }
    }
}
