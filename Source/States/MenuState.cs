
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformGame.Source.Controls;
using System;
using System.Collections.Generic;

namespace PlatformGame.Source.States
{
    public class MenuState : State
    {

        private List<Component> _components;

        public MenuState(ContentManager content, GraphicsDevice graphicsDevice, PlatformerGame game, SpriteBatch spriteBatch) : base(content, graphicsDevice, game, spriteBatch)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont, spriteBatch)
            {
                Position = new Vector2(Constants.ScreenWidth / 2 - buttonTexture.Width / 2, Constants.ScreenHeight / 2 - 100),
                Text = "New Game"
            };

            newGameButton.Click += NewGameButton_Click;

            var creditsButton = new Button(buttonTexture, buttonFont, spriteBatch)
            {
                Position = new Vector2(Constants.ScreenWidth / 2 - buttonTexture.Width / 2, Constants.ScreenHeight / 2),
                Text = "Credits"
            };
            creditsButton.Click += CreditsButton_Click;

            var quitButton = new Button(buttonTexture, buttonFont, spriteBatch)
            {
                Position = new Vector2(Constants.ScreenWidth / 2 - buttonTexture.Width / 2, Constants.ScreenHeight / 2 + 100),
                Text = "Quit"
            };
            quitButton.Click += QuitButton_Click;


            _components = new List<Component>()
            {
                quitButton,
                newGameButton,
                creditsButton

            };
        }

        private void CreditsButton_Click(object sender, EventArgs e)
        {
            //TODO: betere credits
            Console.WriteLine("CREDITS: ROBBE VERMEIRE");
        }


        public override void Draw()
        {
            _spriteBatch.Begin();
            foreach (Component c in _components)
                c.Draw();
            _spriteBatch.End();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new ChooseLevelState(_content, _graphicsDevice, _game, _spriteBatch));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Component c in _components)
                c.Update(gameTime);
        }
        private void QuitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

    }
}
