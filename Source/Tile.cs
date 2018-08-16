using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source
{
    public class Tile : Sprite
    {
        public bool IsBlocked { get; set; }
        

        public Tile(Texture2D TileTexture,Vector2 tilePosition, SpriteBatch spriteBatch, bool isBlocked) : base(TileTexture,tilePosition,spriteBatch)
        {
            isBlocked = IsBlocked;


        }

        public override void Draw()
        {
            if(IsBlocked) base.Draw();

        }
    }
}