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
        }


        public Tile(Texture2D TileTexture, Vector2 tilePosition, SpriteBatch spriteBatch, bool isBlocked) : base(TileTexture, tilePosition, spriteBatch)
        {
            IsBlocked = isBlocked;
        }

        public override void Draw()
        {
            base.Draw();
        }
        public override void Touches(Sprite sprite, Direction? touchedSide=null)
        {
            //Raakt Links/Rechts van de box aan
            if(touchedSide == Direction.Left || touchedSide == Direction.Right)
            {
                sprite.Velocity.X = 0;
            }
            if(touchedSide == Direction.Top || touchedSide == Direction.Bottom)
            {
                sprite.Velocity.Y = 0;
            }

        }
    }
}