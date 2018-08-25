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
        SpriteBatch _spriteBatch;
        Player _player;
        UserMadeBoard _userMadeBoard;
        List<Sprite> _sprites;
        HUD _HUD;
        Camera _camera;
        Game _game;

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
            _camera.FollowSprite();
            //_camera.MoveRight(4);
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
