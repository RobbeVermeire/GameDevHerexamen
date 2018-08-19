using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Source
{
    public class Player : Sprite
    {

        private KeyboardState _keyboardState;

        public Player(Texture2D tex, Vector2 pos, SpriteBatch batch, bool isAnimated=false) : base(tex, pos, batch,isAnimated)
        {
            Acceleration = Constants.Gravity;

            _animation = new Animation();

            _animation.AddFrame(new Rectangle(0,0,72,97));
            _animation.AddFrame(new Rectangle(73,0,72,97));
            _animation.AddFrame(new Rectangle(146,0,72,97));

            _animation.AddFrame(new Rectangle(0,98,72,97));
            _animation.AddFrame(new Rectangle(73,98,72,97));
            _animation.AddFrame(new Rectangle(146,98,72,97));

            _animation.AddFrame(new Rectangle(219,0,72,97));
            _animation.AddFrame(new Rectangle(292,0,72,97));
            _animation.AddFrame(new Rectangle(219,98,72,97));

            _animation.AddFrame(new Rectangle(365,0,72,97));
            _animation.AddFrame(new Rectangle(292,98,72,97));

            CollisionRect = new Rectangle((int)Position.X, (int)Position.Y, 72, 97);
        }

        public override void Update(GameTime gameTime)
        {
            CheckKeyBoardStateAndUpdateMovement(); 
            SimulateFriction();
            MoveIfPossible(gameTime);
            UpdateAnimation(gameTime);
            //System.Diagnostics.Debug.WriteLine("Position:" + Position + " Velocity:" + Velocity + " Acceleration:" + Acceleration);          
        }

        private void UpdateAnimation(GameTime gameTime)
        {
            if (_keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.Right))
                _animation.Update(gameTime);

        }

        private void CheckKeyBoardStateAndUpdateMovement()
        {  
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Left)) { Velocity -= Vector2.UnitX * 5; }
            if (_keyboardState.IsKeyDown(Keys.Right)) { Velocity += Vector2.UnitX * 5; }
            if (_keyboardState.IsKeyDown(Keys.Up) && isOnFirmGround()) { Velocity -= Vector2.UnitY * 80; }
        }
        /// <summary>
        /// Helper functies voor betere code:
        /// </summary>
        private void AffectWithGravity()
        {
            Acceleration = Vector2.UnitY * .5f;
        }

        private bool isOnFirmGround()
        {
            Rectangle oneBelow = CollisionRect;
            oneBelow.Offset(0, 1);
            foreach (Tile t in Board.CurrentBoard.Tiles)
            {
                if (t!= null && oneBelow.Intersects(t.CollisionRect) && t.IsBlocked)
                {
                    return true;
                }
            }
            return false;
        }

        private void SimulateFriction()
        {
            Velocity -= Velocity * Vector2.One * .2f;  
        }

        private void MoveIfPossible(GameTime gameTime)
        {
            Vector2 oldPosition = Position;
            UpdatePosition(gameTime);
            Position = Board.CurrentBoard.WhereCanIGetTo(oldPosition, Position, CollisionRect);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Position += Velocity * deltaTime / 30;
            Velocity += Acceleration * deltaTime / 30;
            CollisionRect.X = (int)Position.X;
            CollisionRect.Y = (int)Position.Y;

        }
    }
}
