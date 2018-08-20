﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Boards;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace PlatformGame.Source
{
    internal class UserMadeBoard : Board
    {
        private int[,] tileGridPosition;
        //TODO: veranderen wanneer finaal tileSet gemaakt is.
        private int[] NonCollideTiles = { 10, 11, 20, 21, 22, 24, 23, 28, 29, 30, 31, 32, 33, 34, 35 };

        public UserMadeBoard(XmlDocument document, Texture2D[] textures, SpriteBatch batch, List<Sprite> sprites) : base(textures, batch)
        {
            tileGridPosition = XmlParser.ToTileGrid(document);
            Columns = tileGridPosition.GetLength(0);
            Rows = tileGridPosition.GetLength(1);
            Tiles = new Tile[Columns, Rows];
            PlaceTiles(Tiles,sprites);
        }
        private void PlaceTiles(Tile[,] tiles,List<Sprite> sprites)
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    Vector2 tilePosition = new Vector2(x * tileWidth, y * tileHeight);

                    if (tileGridPosition[x, y] == 0) continue;

                    if (NonCollideTiles.Contains(tileGridPosition[x, y]))
                        tiles[x, y] = new Tile(TileTextures[tileGridPosition[x, y] - 1], tilePosition, SpriteBatch, false);
                    else
                        tiles[x, y] = new Tile(TileTextures[tileGridPosition[x, y] - 1], tilePosition, SpriteBatch, true);
                    
                    sprites.Add(tiles[x, y]);



                }
            }
        }

    }
}
