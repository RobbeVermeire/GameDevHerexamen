using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source.Controls
{
    public class HUD : Component
    {

        public override SpriteBatch SpriteBatch { get; set; }
        private Player _player;
        private IDictionary<string, Texture2D> _textures;
        private Vector2 topLeftCorner
        {
            get
            {
                return new Vector2(
                    _player.Position.X + _player.CollisionRect.Width / 2 - Constants.ScreenWidth / 2
                    , _player.Position.Y - Constants.ScreenHeight / 2
                    );
            }
        }

        public HUD(IDictionary<string,Texture2D> textures, SpriteBatch batch, Player player)
        {
            SpriteBatch = batch;
            _textures = textures;
            _player = player;

        }


        public override void Draw()
        {
            DrawHealth();

        }

        private void DrawHealth()
        {
            Texture2D fullHeart = _textures["FullHeart"];
            Texture2D halfHeart = _textures["HalfHeart"];
            for(int i = 0; i < _player.Health; i++)
            {
                Vector2 drawPos = topLeftCorner + new Vector2(20 * i, 0);
                if (i+2 <= _player.Health)
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
