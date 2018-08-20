using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source
{
    public class Tile : Sprite
    {
        public bool IsBlocked { get; set; }
        public override Rectangle CollisionRect
        {
            get {
                if (IsBlocked)
                    return base.CollisionRect;
                else
                    return Rectangle.Empty;
                }
            set
            {
                base.CollisionRect = value;
            }
        }


        public Tile(Texture2D TileTexture, Vector2 tilePosition, SpriteBatch spriteBatch, bool isBlocked) : base(TileTexture, tilePosition, spriteBatch)
        {
            IsBlocked = isBlocked;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}