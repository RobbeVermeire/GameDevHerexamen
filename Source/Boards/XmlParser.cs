using System.Xml;

namespace PlatformGame.Source.Boards
{
    public static class XmlParser
    {
        public static int[,] ToTileGrid(XmlDocument xmlDocument)
        {
            string tileDataString;
            int rows;
            int columns;
            int[,] tileGridPosition;
            try
            {
                //Rijen & kollomen uit Xml bestand halen:
                rows = int.Parse(xmlDocument.ChildNodes[1].Attributes.GetNamedItem("height").Value);
                columns = int.Parse(xmlDocument.ChildNodes[1].Attributes.GetNamedItem("width").Value);
                tileGridPosition = new int[columns, rows];

                //Haal level data uit XML Document:
                tileDataString = xmlDocument.GetElementsByTagName("data")[0].InnerText;
            }
            catch (XmlException ex)
            {
                //TODO: handle error
                return null;
            }

            //Verwijder ongewenste characters uit string met data:
            string[] charsToRemove = new string[] { "\n", "\r" };
            foreach (string s in charsToRemove)
            {
                tileDataString = tileDataString.Replace(s, string.Empty);
            }

            //Split Array zodat kommas weg zijn:
            string[] tileDataSplitArray = tileDataString.Split(',');

            //Converteer 1D Array naar 2D Array:
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    tileGridPosition[x, y] = int.Parse(tileDataSplitArray[x + y * columns]);
                }
            }

            return tileGridPosition;
        }
        public static string[] ToTextureArray(XmlDocument xmlDocument)
        {
            //+1 voor airTile die niet in tileset zit
            int tileCount = int.Parse(xmlDocument.ChildNodes[1].Attributes.GetNamedItem("tilecount").Value);
            string[] textures = new string[tileCount];

            for (int i = 1; i < tileCount + 1; i++)
            {
                //TODO : try/catch block
                string textureSource = xmlDocument.ChildNodes[1].ChildNodes[i].FirstChild.Attributes.GetNamedItem("source").Value.Substring(9);
                textureSource = textureSource.Replace(".png", string.Empty);
                textures[i - 1] = textureSource;
            }
            return textures;
        }
    }
}
