using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Boards;

namespace PlatformGame.Source.States
{
    public class GameState : State
    {

        List<Component> _components;
        Player _player;
        Board _board;
        Camera _camera;
        XmlDocument _mapFile;
        XmlDocument _tileSet;
        private Color _backGroundColor;

        string[] _textureSources;
        Texture2D[] _textures;
        Rectangle viewPort;
        public GameState(ContentManager content, GraphicsDevice graphicsDevice, PlatformerGame game, SpriteBatch spriteBatch) : base(content, graphicsDevice, game,spriteBatch)
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
            _board = new UserMadeBoard(_mapFile, _textures, _spriteBatch);
            _player = new Player(_content.Load<Texture2D>("Player/p1_spritesheet"), new Vector2(100, 0), _spriteBatch, true);

            _components = new List<Component>
            {
                _player,
            };

            viewPort = new Rectangle(Point.Zero, new Point(Constants.ScreenWidth, Constants.ScreenHeight));
            _backGroundColor = new Color(new Vector3(208, 244, 247));

    }
        public override void Update(GameTime gameTime)
        {
            foreach (Component component in _components)
                component.Update(gameTime);
            _camera.Follow(_player);
        }

        public override void Draw()
        {
            _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);
            _game.GraphicsDevice.Clear(_backGroundColor);
            _board.Draw();
            foreach (Component component in _components)
                component.Draw();
            _spriteBatch.End();
        }


    }
}
