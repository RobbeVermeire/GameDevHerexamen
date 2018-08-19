using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Source;
using PlatformGame.Source.Boards;
using PlatformGame.Source.States;
using System.Collections.Generic;
using System.Xml;

namespace PlatformGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PlatformerGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private State _currentState;
        private State _nextState;
        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }
        protected override void LoadContent() 
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(Content, _graphics.GraphicsDevice, this, _spriteBatch);


        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if(_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }
            _currentState.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Beige);

            _currentState.Draw();

            base.Draw(gameTime);
        }
    }
}
