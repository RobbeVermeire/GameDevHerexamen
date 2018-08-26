using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Source.Managers
{
    public class LevelManager : State
    {
        public Level CurrentLevel;
        public int LevelCount;
        public List<Level> Levels;
        public SpriteBatch SpriteBatch;
        private int _difficulty;

        public LevelManager(List<Level> levels, int difficulty,ContentManager content,GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, PlatformerGame game ):base(content,graphicsDevice, game,spriteBatch)
        {
            LevelCount = 0;
            Levels = levels;
            CurrentLevel = Levels[LevelCount];
            SpriteBatch = spriteBatch;
            _game = game;
            _difficulty = difficulty;
        }


        public override void Draw()
        {
            SpriteBatch.Begin(transformMatrix: CurrentLevel._camera.TransformMatrix);
            CurrentLevel._game.GraphicsDevice.Clear(new Color(new Vector3(208, 250, 250)));
            foreach (Sprite sprite in CurrentLevel._sprites)
                sprite.Draw();
            CurrentLevel._HUD.Draw();
            SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Sprite sprite in CurrentLevel._sprites)
                sprite.Update(gameTime, CurrentLevel._sprites);
            CurrentLevel._camera.MoveRight(_difficulty);

            if(CurrentLevel._player.Position.X >= 6630)
            {
                _game.ChangeState(new GameState2(_content, _graphicsDevice, _game, _spriteBatch, _difficulty));
            }
            if ((CurrentLevel._player.Position.X <= -CurrentLevel._camera.Position.X - Constants.ScreenWidth) ||
                (CurrentLevel._player.Position.X >= -CurrentLevel._camera.Position.X + Constants.ScreenWidth))
            {
                _game.ChangeState(new GameState1(_content, _graphicsDevice, _game, _spriteBatch, _difficulty));
            }
        }

    }
}
