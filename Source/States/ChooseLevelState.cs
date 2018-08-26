using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Controls;

namespace PlatformGame.Source.States
{
    public class ChooseLevelState : State
    {
        private List<Component> _components;
        private int _difficulty;

        public ChooseLevelState(ContentManager content, GraphicsDevice graphicsDevice, PlatformerGame game, SpriteBatch spriteBatch) : base(content, graphicsDevice, game, spriteBatch)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            _difficulty = 3;

            var easyButton = new Button(buttonTexture, buttonFont, spriteBatch)
            {
                Position = new Vector2(80, 100),
                Text = "Easy"
            };
            easyButton.Click += EasyButton_Click;
            var mediumButton = new Button(buttonTexture, buttonFont, spriteBatch)
            {
                Position = new Vector2(80 + buttonTexture.Width, 100),
                Text = "Medium"
            };
            mediumButton.Click += MediumButton_Click;
            var hardButton = new Button(buttonTexture, buttonFont, spriteBatch)
            {
                Position = new Vector2(80 +2* buttonTexture.Width, 100),
                Text = "Hard"
            };
            hardButton.Click += HardButton_Click;

            var level1Button = new Button(buttonTexture, buttonFont, spriteBatch)
            {
                Position = new Vector2(Constants.ScreenWidth / 2 - buttonTexture.Width / 2, Constants.ScreenHeight / 2 - 100),
                Text = "Level 1"
            };

            level1Button.Click += level1Button_Click;

            var level2Button = new Button(buttonTexture, buttonFont, spriteBatch)
            {
                Position = new Vector2(Constants.ScreenWidth / 2 - buttonTexture.Width / 2, Constants.ScreenHeight / 2),
                Text = "Level 2"
            };
            level2Button.Click += level2Button_Click;

            var quitButton = new Button(buttonTexture, buttonFont, spriteBatch)
            {
                Position = new Vector2(Constants.ScreenWidth / 2 - buttonTexture.Width / 2, Constants.ScreenHeight / 2 + 100),
                Text = "Quit"
            };
            quitButton.Click += QuitButton_Click;

            _components = new List<Component>()
            {
                easyButton,
                mediumButton,
                hardButton,
                level1Button,
                level2Button,
                quitButton,
            };

        }

        private void HardButton_Click(object sender, EventArgs e)
        {
            _difficulty = 9;
            Console.WriteLine("Difficulty = Hard");
        }

        private void MediumButton_Click(object sender, EventArgs e)
        {
            _difficulty = 6;
            Console.WriteLine("Difficulty = Medium");
        }

        private void EasyButton_Click(object sender, EventArgs e)
        {
            _difficulty = 3;
            Console.WriteLine("Difficulty = Easy");
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void level1Button_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState1(_content, _graphicsDevice, _game, _spriteBatch,_difficulty));
        }
        private void level2Button_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState2(_content, _graphicsDevice, _game, _spriteBatch,_difficulty));
        }

        public override void Draw()
        {
            _spriteBatch.Begin();
            foreach (Component c in _components)
                c.Draw();
            _spriteBatch.End();

        }

        public override void Update(GameTime gameTime)
        {
            foreach (Component c in _components)
                c.Update(gameTime);
        }
    }
}
