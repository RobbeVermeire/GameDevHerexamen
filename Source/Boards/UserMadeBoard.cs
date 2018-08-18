using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PlatformGame.Source
{
    class UserMadeBoard: Board
    {
        private int[,] tileGridPosition;

        public UserMadeBoard(XmlDocument document, Texture2D[] textures, SpriteBatch batch):base(textures,batch)
        {
            tileGridPosition = XmlParser.ToTileGrid(document);
            Columns = tileGridPosition.GetLength(0);
            Rows = tileGridPosition.GetLength(1);
            Tiles = new Tile[Columns, Rows];
            PlaceTiles(Tiles);
        }
        private void PlaceTiles(Tile[,] tiles)
        {
            for(int y= 0; y < Rows; y++)
            {
                for(int x=0; x < Columns; x++)
                {
                    Vector2 tilePosition = new Vector2(x * tileWidth, y * tileHeight);   
                    if (tileGridPosition[x, y] == 0)
                        tiles[x, y] = new Tile(TileTextures[tileGridPosition[x,y]], tilePosition, SpriteBatch, false);
                    else
                        tiles[x, y] = new Tile(TileTextures[tileGridPosition[x, y]], tilePosition, SpriteBatch, true);

                }
            }
        }
        
    }
}
