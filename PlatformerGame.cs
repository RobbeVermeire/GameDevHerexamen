using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Source;
using System.Collections.Generic;
using System.Xml;

namespace PlatformGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PlatformerGame : Game
    {
        List<Component> _components;
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Player _player;
        RandomBoard _board;
        Camera _camera;
        XmlDocument _document;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent() 
        {
            _document = new XmlDocument();
            _document.Load("Maps/Test Map.tmx");
            string locale = _document.InnerText.ToString();

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new Camera();

            _board = new RandomBoard(30, 10, Content.Load<Texture2D>("Tiles/grassMid"), _spriteBatch);
            _player = new Player(Content.Load<Texture2D>("Player/p2_stand"), new Vector2(100,100), _spriteBatch);

            _components = new List<Component>
            {
                _player,

            };

        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (Component component in _components)
                component.Update(gameTime);
            _camera.Follow(_player);


            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);
            _board.Draw();
            foreach (Component component in _components)
                component.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
