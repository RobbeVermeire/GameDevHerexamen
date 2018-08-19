using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PlatformGame.Source
{
    internal class RandomBoard : Board
    {
        //TODO: Remove random voor level design
        private Random _rnd = new Random();

        public RandomBoard(int columns, int rows, Texture2D[] textures, SpriteBatch batch) : base(textures, batch)
        {
            Columns = columns;
            Rows = rows;
            GenerateRandomBlockedWorld();
            SetAllBorderTilesBlocked();
        }

        private void GenerateRandomBlockedWorld()
        {
            Tiles = new Tile[Columns, Rows];
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    Vector2 tilePosition = new Vector2(x * tileWidth, y * tileHeight);
                    Tiles[x, y] = new Tile(TileTextures[_rnd.Next(TileTextures.Length)], tilePosition, SpriteBatch, _rnd.Next(4) == 0);
                }

            }
        }

        private void SetAllBorderTilesBlocked()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if (x == 0 || y == 0 || y == Rows - 1 || (x == 2 && y == 3))
                    {
                        Tiles[x, y].IsBlocked = true;
                    }
                }
            }
        }
    }
}
