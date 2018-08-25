using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PlatformGame.Source.Controls
{
    public class HUD : Component
    {

        public override SpriteBatch SpriteBatch { get; set; }
        public Camera Camera { get; set; }
        private Player _player;
        private IDictionary<string, Texture2D> _textures;
        private Vector2 topLeftCorner
        {
            get
            {
                return new Vector2(-Camera.Position.X, -Camera.Position.Y)
                    - new Vector2(Constants.ScreenWidth / 2 - 50, Constants.ScreenHeight * (0.60f) - 50);
            }
        }

        public HUD(IDictionary<string, Texture2D> textures, SpriteBatch batch, Player player, Camera camera)
        {
            SpriteBatch = batch;
            _textures = textures;
            Camera = camera;
            _player = player;
        }


        public override void Draw()
        {
            DrawHealth();
            DrawCoins();
        }

        private void DrawCoins()
        {
            Texture2D coin = _textures["Coin"];
            Texture2D cross = _textures["Cross"];
            SpriteBatch.Draw(coin, topLeftCorner + new Vector2(Constants.ScreenWidth - 200, 0), Color.White);
            SpriteBatch.Draw(cross, topLeftCorner + new Vector2(Constants.ScreenWidth - 150, 10), Color.White);
            int coins = _player.Coins;
            List<int> digitList = new List<int>();
            while (coins > 0)
            {
                digitList.Add(coins % 10);
                coins = coins / 10;
            }

            //digitList.Reverse();
            for (int i = 0; i < digitList.Count; i++)
            {
                string digit = string.Format("{0}", digitList[i]);
                SpriteBatch.Draw(_textures[digit], topLeftCorner + new Vector2(Constants.ScreenWidth - _textures["0"].Width * (i + 3), 5), Color.White);
            }


        }

        private void DrawHealth()
        {
            Texture2D fullHeart = _textures["FullHeart"];
            Texture2D halfHeart = _textures["HalfHeart"];
            for (int i = 0; i < _player.Health; i++)
            {
                Vector2 drawPos = topLeftCorner + new Vector2(20 * i, 0);
                if (i + 2 <= _player.Health)
                {
                    SpriteBatch.Draw(fullHeart, drawPos, Color.White);
                    i++;
                }
                else
                {
                    SpriteBatch.Draw(halfHeart, drawPos, Color.White);
                }
            }

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
