using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Boards;
using System.Collections.Generic;
using System.Xml;

namespace PlatformGame.Source.States
{
    public class GameState : State
    {
        private List<Sprite> _sprites;
        private Player _player;
        private Board _board;
        private Camera _camera;
        private XmlDocument _mapFile;
        private XmlDocument _tileSet;
        private Color _backGroundColor;

        private string[] _textureSources;
        private Texture2D[] _textures;
        private Rectangle viewPort;

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, PlatformerGame game, SpriteBatch spriteBatch) : base(content, graphicsDevice, game, spriteBatch)
        {

            content.RootDirectory = "Content";
            _tileSet = new XmlDocument();
            _mapFile = new XmlDocument();

            _mapFile.Load("../../../../Content/Maps/TileTest.tmx");
            _tileSet.Load("../../../../Content/Maps/TileSet.tsx");

            _textureSources = XmlParser.ToTextureDictionary(_tileSet);
            _textures = new Texture2D[_textureSources.Length];


            _camera = new Camera();

            //Source dictionary omzetten naar een texture dictionary : (int,string) --> (int,Texture2d)
            for (int i = 0; i < _textureSources.Length; i++)
            {
                _textures[i] = _content.Load<Texture2D>("Tiles/" + _textureSources[i]);
            }



            //_board = new RandomBoard(30, 20, Content.Load<Texture2D>("Tiles/box"), _spriteBatch);
            _sprites = new List<Sprite>();
            _board = new UserMadeBoard(_mapFile, _textures, _spriteBatch, _sprites);
            _player = new Player(_content.Load<Texture2D>("Player/p1_spritesheet"), new Vector2(100, 0), _spriteBatch, true);
            _sprites.Add(_player);

            viewPort = new Rectangle(Point.Zero, new Point(Constants.ScreenWidth, Constants.ScreenHeight));
            _backGroundColor = new Color(new Vector3(208, 244, 247));

        }
        public override void Update(GameTime gameTime)
        {
            foreach (Sprite sprite in _sprites)
                sprite.Update(gameTime,_sprites);
            _camera.Follow(_player);
        }

        public override void Draw()
        {
            _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);
            _game.GraphicsDevice.Clear(_backGroundColor);
            //_board.Draw();
            foreach (Sprite sprite in _sprites)
                sprite.Draw();
            _spriteBatch.End();
        }


    }
}
