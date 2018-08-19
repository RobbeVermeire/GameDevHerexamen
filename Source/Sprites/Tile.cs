using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source
{
    public class Tile : Sprite
    {
        public bool IsBlocked { get; set; }


        public Tile(Texture2D TileTexture, Vector2 tilePosition, SpriteBatch spriteBatch, bool isBlocked) : base(TileTexture, tilePosition, spriteBatch)
        {
            IsBlocked = isBlocked;
            if (!IsBlocked)
                CollisionRect = Rectangle.Empty;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}