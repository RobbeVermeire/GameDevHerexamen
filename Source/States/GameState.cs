using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Boards;
using PlatformGame.Source.Sprites;
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
        private Dictionary<string, Animation> _playerAnimations;
        private Fly _fly;

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
            _playerAnimations = new Dictionary<string, Animation>
            {
                {"WalkRight", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet"),11,
                new List<Rectangle>
                {
                    new Rectangle(0, 0, 72, 97),
                    new Rectangle(73, 0, 72, 97),
                    new Rectangle(146, 0, 72, 97),
                    new Rectangle(0, 98, 72, 97),
                    new Rectangle(73, 98, 72, 97),
                    new Rectangle(146, 98, 72, 97),
                    new Rectangle(219, 0, 72, 97),
                    new Rectangle(292, 0, 72, 97),
                    new Rectangle(219, 98, 72, 97),
                    new Rectangle(365, 0, 72, 97),
                    new Rectangle(292, 98, 72, 97),
                })},

                {"Stand", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet"),1,
                new List<Rectangle>
                {
                    new Rectangle(67,196,72,97)
                })},
                {"Jump", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet"),1,
                new List<Rectangle>
                {
                    new Rectangle(438,93,72,97)
                }
                )},
            };
            _fly = new Fly(_content.Load<Texture2D>("Enemies/enemies_spritesheet"), new Vector2(1150, 564), _spriteBatch,
                new Dictionary<string, Animation>
                {
                    {"Right", new Animation(_content.Load<Texture2D>("Enemies/enemies_spritesheet"),2,
                    new List<Rectangle>
                    {
                    new Rectangle(0,32,72,36),
                    new Rectangle(0,0, 75, 31)
                    }
                )},

                });
            _sprites = new List<Sprite>();
            _board = new UserMadeBoard(_mapFile, _textures, _spriteBatch, _sprites);
            _player = new Player(_content.Load<Texture2D>("Player/p3_spritesheet"), new Vector2(100, 600), _spriteBatch,_playerAnimations);
            _sprites.Add(_player);
            _sprites.Add(_fly);

            viewPort = new Rectangle(Point.Zero, new Point(Constants.ScreenWidth, Constants.ScreenHeight));
            _backGroundColor = new Color(new Vector3(208, 250, 250));

        }
        public override void Update(GameTime gameTime)
        {
            foreach (Sprite sprite in _sprites)
                sprite.Update(gameTime, _sprites);
            _camera.Follow(_player);
        }

        public override void Draw()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_content.Load<Texture2D>("hud/hud_heartFull"), new Vector2(200, 500), Color.White);
            _spriteBatch.End();
            _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);
            _game.GraphicsDevice.Clear(_backGroundColor);
            //_board.Draw();
            foreach (Sprite sprite in _sprites)
                sprite.Draw();
            _spriteBatch.End();
        }


    }
}
