using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Boards;
using PlatformGame.Source.Controls;
using PlatformGame.Source.Sprites;
using System;
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

        private Vector2 _hudOffset;
        private HUD _HUD;

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, PlatformerGame game, SpriteBatch spriteBatch) : base(content, graphicsDevice, game, spriteBatch)
        {

            content.RootDirectory = "Content";
            _tileSet = new XmlDocument();
            _mapFile = new XmlDocument();

            _mapFile.Load("../../../../Content/Maps/Level1.tmx");
            _tileSet.Load("../../../../Content/Maps/TileSetLevel1.tsx");

            _textureSources = XmlParser.ToTextureDictionary(_tileSet);
            _textures = new Texture2D[_textureSources.Length];

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


                {"WalkLeft", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet_mirrored"),11,
                new List<Rectangle>
                {
                    new Rectangle(508-72, 0, 72, 97),
                    new Rectangle(508-73-72, 0, 72, 97),
                    new Rectangle(508-146-72, 0, 72, 97),
                    new Rectangle(508-0-72, 98, 72, 97),
                    new Rectangle(508-73-72, 98, 72, 97),
                    new Rectangle(508-146-72, 98, 72, 97),
                    new Rectangle(508-219-72, 0, 72, 97),
                    new Rectangle(508-292-72, 0, 72, 97),
                    new Rectangle(508-219-72, 98, 72, 97),
                    new Rectangle(508-365-72, 0, 72, 97),
                    new Rectangle(508-292-72, 98, 72, 97),
                }
                )},

                {"StandRight", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet"),1,
                new List<Rectangle>
                {
                    new Rectangle(67,196,72,97)
                })},
                {"StandLeft", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet_mirrored"),1,
                new List<Rectangle>
                {
                    new Rectangle(508-67-72,196,72,97)
                }
                )},

                {"JumpRight", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet"),1,
                new List<Rectangle>
                {
                    new Rectangle(438,93,72,97)
                }
                )},
                {"JumpLeft", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet_mirrored"),1,
                new List<Rectangle>
                {
                    new Rectangle(508-438-72,93,72,97)
                }
                )},

                {"HurtRight", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet"),1,
                new List<Rectangle>
                {
                    new Rectangle(438, 0, 69, 92)
                }
                )},
                {"HurtLeft", new Animation(_content.Load<Texture2D>("Player/p3_spritesheet_mirrored"),1,
                new List<Rectangle>
                {
                    new Rectangle(508-438-69, 0, 69, 92)
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
                    {"Left", new Animation(_content.Load<Texture2D>("Enemies/enemies_spritesheet_mirrored"),2,
                    new List<Rectangle>
                    {
                    new Rectangle(281,32,72,36),
                    new Rectangle(276,0, 75, 31)
                    }
                )},

                });
            _sprites = new List<Sprite>();
            _board = new UserMadeBoard(_mapFile, _textures, _spriteBatch, _sprites);
            _player = new Player(_content.Load<Texture2D>("Player/p3_spritesheet"), new Vector2(100, 600), _spriteBatch, _playerAnimations);
            _sprites.Add(_player);
            _sprites.Add(_fly);
            _camera = new Camera(_player);

            _HUD = new HUD(
                new Dictionary<string, Texture2D>
                {
                    {"FullHeart", _content.Load<Texture2D>("HUD/hud_heartFull")  },
                    {"HalfHeart", _content.Load<Texture2D>("HUD/hud_heartHalf")}
                },
                _spriteBatch,
                _player,
                _camera);
            _hudOffset = new Vector2(-Constants.ScreenWidth / 2 + _player.CollisionRect.Width, - Constants.ScreenHeight / 2);

            viewPort = new Rectangle(Point.Zero, new Point(Constants.ScreenWidth, Constants.ScreenHeight));
            _backGroundColor = new Color(new Vector3(208, 250, 250));

        }
        public override void Update(GameTime gameTime)
        {
            foreach (Sprite sprite in _sprites)
                sprite.Update(gameTime, _sprites);
            _camera.MoveRight(4);

            //_camera.FollowSprite();
        }

        public override void Draw()
        {
            _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);
            _game.GraphicsDevice.Clear(_backGroundColor);
            foreach (Sprite sprite in _sprites)
                sprite.Draw();
            _HUD.Draw();
            _spriteBatch.End();
        }


    }
}
