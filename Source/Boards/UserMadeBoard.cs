using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Boards;
using PlatformGame.Source.Sprites;
using PlatformGame.Source.Tiles;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace PlatformGame.Source
{
    public class UserMadeBoard : Board
    {
        private int[,] tileGridPosition;
        //TODO: veranderen wanneer finaal tileSet gemaakt is.
        private int[] _nonCollideTiles;
        private int _coinTile;
        private int[] _bridgeTiles;
        private int[] _killTiles;
 
        public UserMadeBoard(XmlDocument document, Texture2D[] textures, SpriteBatch batch, List<Sprite> sprites, int[] nonCollideTiles, int coinTiles, int[] bridgeTiles, int[] killTiles) : base(textures, batch)
        {
            _nonCollideTiles = nonCollideTiles;
            _coinTile = coinTiles;
            _bridgeTiles = bridgeTiles;
            _killTiles = killTiles;
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

                    if (_nonCollideTiles.Contains(tileGridPosition[x, y]))
                        tiles[x, y] = new Tile(TileTextures[tileGridPosition[x, y] - 1], tilePosition, SpriteBatch, false);
                    else if (_bridgeTiles.Contains(tileGridPosition[x, y]))
                        tiles[x, y] = new Bridge(TileTextures[tileGridPosition[x, y] - 1], tilePosition, SpriteBatch, true);
                    else if (tileGridPosition[x, y] == _coinTile)
                        tiles[x, y] = new Coin(TileTextures[tileGridPosition[x, y] - 1], tilePosition, SpriteBatch);
                    else if (_killTiles.Contains(tileGridPosition[x, y]))
                        tiles[x, y] = new KillTile(TileTextures[tileGridPosition[x, y] - 1], tilePosition, SpriteBatch,true);
                    else
                        tiles[x, y] = new Tile(TileTextures[tileGridPosition[x, y] - 1], tilePosition, SpriteBatch, true);
                    
                    sprites.Add(tiles[x, y]);
                }
            }
        }

    }
}
