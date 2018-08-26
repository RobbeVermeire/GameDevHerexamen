using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Boards;
using PlatformGame.Source.Controls;
using PlatformGame.Source.Enemies;
using PlatformGame.Source.Managers;
using PlatformGame.Source.Sprites;
using System.Collections.Generic;
using System.Xml;

namespace PlatformGame.Source.States
{
    public class GameState1 : State
    {
        private List<Level> levels;
        private LevelManager levelManager;
        public GameState1(ContentManager content, GraphicsDevice graphicsDevice, PlatformerGame game, SpriteBatch spriteBatch,int difficulty) : base(content, graphicsDevice, game, spriteBatch)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
            _game = game;
            _spriteBatch = spriteBatch;
            _content.RootDirectory = "Content";

            #region Level1
            #region Sprites
            List<Sprite>  _sprites = new List<Sprite>();
            Fly _fly = new Fly(_content.Load<Texture2D>("Enemies/enemies_spritesheet"), new Vector2(1150, 564), _spriteBatch,
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
            Fly _fly2 = new Fly(_content.Load<Texture2D>("Enemies/enemies_spritesheet"), new Vector2(4200, 700), _spriteBatch,
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
            Player _player = new Player(_content.Load<Texture2D>("Player/p3_spritesheet"), new Vector2(100, 600), _spriteBatch,
                new Dictionary<string, Animation>
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


            });
            _sprites.Add(_player);
            _sprites.Add(_fly);
            _sprites.Add(_fly2);
            #endregion
            #region LoadMap
            XmlDocument _tileSet = new XmlDocument();
            XmlDocument _mapFile = new XmlDocument();

            _mapFile.Load("../../../../Content/Maps/Level1.tmx");
            _tileSet.Load("../../../../Content/Maps/TileSetLevel1.tsx");
            string[] _tileTextureSources = XmlParser.ToTextureArray(_tileSet);
            Texture2D[] _tileTextures = new Texture2D[_tileTextureSources.Length];
            //Source dictionary omzetten naar een texture array : (int,string) --> (int,Texture2d)
            for (int i = 0; i < _tileTextureSources.Length; i++)
            {
                _tileTextures[i] = _content.Load<Texture2D>("Tiles/" + _tileTextureSources[i]);
            }
            int[] nonCollideTiles = { 14, 15, 16, 17, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 42, 43, 44, 45, 46, 47, 48, 49 };
            int _coinTile = 40; 
            int[] _bridgeTiles = { 5 };
            int[] _killTiles = { 20, 21 };
            UserMadeBoard _userMadeBoard = new UserMadeBoard(_mapFile, _tileTextures, _spriteBatch, _sprites, nonCollideTiles,_coinTile,_bridgeTiles,_killTiles);
            #endregion
            #region Camera&Hud
            Camera _camera = new Camera(_player);

            HUD _HUD = new HUD(
                new Dictionary<string, Texture2D>
                {
                    {"FullHeart", _content.Load<Texture2D>("HUD/hud_heartFull")  },
                    {"HalfHeart", _content.Load<Texture2D>("HUD/hud_heartHalf")},
                    {"Coin", _content.Load<Texture2D>("HUD/hud_coins")},
                    {"Cross", _content.Load<Texture2D>("HUD/hud_x")},
                    {"1", _content.Load<Texture2D>("HUD/hud_1")},
                    {"2", _content.Load<Texture2D>("HUD/hud_2")},
                    {"3", _content.Load<Texture2D>("HUD/hud_3")},
                    {"4", _content.Load<Texture2D>("HUD/hud_4")},
                    {"5", _content.Load<Texture2D>("HUD/hud_5")},
                    {"6", _content.Load<Texture2D>("HUD/hud_6")},
                    {"7", _content.Load<Texture2D>("HUD/hud_7")},
                    {"8", _content.Load<Texture2D>("HUD/hud_8")},
                    {"9", _content.Load<Texture2D>("HUD/hud_9")},
                    {"0", _content.Load<Texture2D>("HUD/hud_0")},
                },
                _spriteBatch,
                _player,
                _camera);
            #endregion
            #endregion

            levels = new List<Level>
            {
                new Level(_spriteBatch,_game,_userMadeBoard,_sprites,_HUD),
            };
            levelManager = new LevelManager(levels, difficulty, _content,_graphicsDevice, _spriteBatch, _game);
        }
        public override void Update(GameTime gameTime)
        {
            levelManager.Update(gameTime);
        }

        public override void Draw()
        {
            levelManager.Draw();
        }
    }
}
