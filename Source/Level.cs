using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Boards;
using PlatformGame.Source.Controls;
using PlatformGame.Source.Enemies;
using PlatformGame.Source.Sprites;
using PlatformGame.Source.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PlatformGame.Source
{
    public class Level
    {
        public SpriteBatch _spriteBatch;
        public Player _player;
        public UserMadeBoard _userMadeBoard;
        public List<Sprite> _sprites;
        public HUD _HUD;
        public Camera _camera;
        public Game _game;

        public Level(SpriteBatch spriteBatch,Game game, UserMadeBoard userMadeBoard, List<Sprite> sprites,HUD hUD)
        {
            _spriteBatch = spriteBatch;
            _userMadeBoard = userMadeBoard;
            _sprites = sprites;
            _player = (Player)sprites.First();
            _camera = new Camera(_player);
            _game = game;
            _HUD = hUD;
            _HUD.Camera = _camera;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Sprite sprite in _sprites)
                sprite.Update(gameTime, _sprites);
        }

        public void Draw()
        {
            _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);
            _game.GraphicsDevice.Clear(new Color(new Vector3(208, 250, 250)));
            foreach (Sprite sprite in _sprites)
                sprite.Draw();
            _HUD.Draw();
            _spriteBatch.End();

        }
    }
}
