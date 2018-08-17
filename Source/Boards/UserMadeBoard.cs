using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private XmlDocument mDocument;
        private int[,] tileGridPosition;

        public UserMadeBoard(XmlDocument document, Texture2D tex, SpriteBatch batch):base(tex,batch)
        {
            mDocument = document;
            Rows = int.Parse(mDocument.ChildNodes[1].Attributes.GetNamedItem("height").Value);
            Columns = int.Parse(mDocument.ChildNodes[1].Attributes.GetNamedItem("width").Value);
            tileGridPosition = new int[Columns,Rows];
            ConvertStringDataToIntArray();
            PlaceTiles();
        }

        private void ConvertStringDataToIntArray()
        {
            string[] charsToRemove = new string[] { "\n" , "\r" };
            //Haal level data uit XML Document:
            string tileDataString = mDocument.GetElementsByTagName("data")[0].InnerText;
            //Split Array zodat kommas weg zijn:
            string[] tileDataSplitArray = tileDataString.Split(',');
            //Converteer 1D Array naar 2D Array:
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    tileGridPosition[x, y] = int.Parse(tileDataSplitArray[x + y * Columns]);
                }
            }
        }

        private void PlaceTiles()
        {
            Tiles = new Tile[Columns, Rows];
            for(int y= 0; y < Rows; y++)
            {
                for(int x=0; x < Columns; x++)
                {
                    Vector2 tilePosition = new Vector2(x * tileWidth, y * tileHeight);

                    if (tileGridPosition[x, y] == 0)
                        Tiles[x, y] = new Tile(TileTexture, tilePosition, SpriteBatch, false);
                    else
                        Tiles[x, y] = new Tile(TileTexture, tilePosition, SpriteBatch, true);

                }
            }
        }
        
    }
}
