using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source
{
    public abstract class Board
    {
        public Tile[,] Tiles { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        protected const int tileWidth = 70;
        protected const int tileHeight = 70;

        protected SpriteBatch SpriteBatch { get; set; }
    }
}