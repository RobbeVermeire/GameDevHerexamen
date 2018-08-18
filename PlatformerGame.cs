using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformGame.Source;
using PlatformGame.Source.Boards;
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
        Board _board;
        Camera _camera;
        XmlDocument _mapFile;
        XmlDocument _tileSet;

        string[] _textureSources;
        Texture2D[] _textures;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            _tileSet = new XmlDocument();
            _mapFile = new XmlDocument();

            _mapFile.Load("../../../../Content/Maps/Test Map.tmx");
            _tileSet.Load("../../../../Content/Maps/Basic Tileset.tsx");

            _textureSources=XmlParser.ToTextureDictionary(_tileSet);
            _textures = new Texture2D[_textureSources.Length];

            base.Initialize();
        }
        protected override void LoadContent() 
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera = new Camera();

            //Source dictionary omzetten naar een texture dictionary : (int,string) --> (int,Texture2d)
            for(int i = 0; i < _textureSources.Length; i++)
            {
                _textures[i] = Content.Load<Texture2D>("Tiles/"+_textureSources[i]);
            }

            

            //_board = new RandomBoard(30, 20, Content.Load<Texture2D>("Tiles/box"), _spriteBatch);
            _board = new UserMadeBoard(_mapFile, _textures,_spriteBatch);
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

            _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);
            _board.Draw();
            foreach (Component component in _components)
                component.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
