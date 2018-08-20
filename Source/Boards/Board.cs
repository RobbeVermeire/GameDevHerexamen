using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source
{
    public abstract class Board
    {
        protected const int tileWidth = 70;
        protected const int tileHeight = 70;

        public Tile[,] Tiles { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }
        public static Board CurrentBoard { get; protected set; }
        public Texture2D[] TileTextures { get; set; }

        public Board(Texture2D[] textures, SpriteBatch batch)
        {
            TileTextures = textures;
            SpriteBatch = batch;
            CurrentBoard = this;
        }
        private Rectangle CreateRectangleAtPosition(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        public void Draw()
        {
            foreach (var tile in Tiles)
            {
                if (tile != null)
                    tile.Draw();
            }
        }

    }
}
