using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Source.States
{
    public abstract class State
    {

        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected PlatformerGame _game;
        protected SpriteBatch _spriteBatch;


        protected State(ContentManager content, GraphicsDevice graphicsDevice, PlatformerGame game, SpriteBatch spriteBatch)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
            _game = game;
            _spriteBatch = spriteBatch;
        }

        public abstract void Draw();
        public abstract void Update(GameTime gameTime);
        
    }
}
