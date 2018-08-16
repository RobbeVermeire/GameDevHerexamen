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

        private KeyboardState keyboardState;

        public Player(Texture2D tex, Vector2 pos, SpriteBatch batch) : base(tex, pos, batch)
        {
            Acceleration = Constants.Gravity;
        }

        public override void Update(GameTime gameTime)
        {
            CheckKeyBoardStateAndUpdateMovement(); 
            SimulateFriction();
            MoveIfPossible(gameTime);
            System.Diagnostics.Debug.WriteLine("Position:" + Position + " Velocity:" + Velocity + " Acceleration:" + Acceleration);          
        }


        private void CheckKeyBoardStateAndUpdateMovement()
        {
            
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left)) { Velocity -= Vector2.UnitX * 10; }
            if (keyboardState.IsKeyDown(Keys.Right)) { Velocity += Vector2.UnitX * 10; }
            if (keyboardState.IsKeyDown(Keys.Up) && isOnFirmGround()) { Velocity -= Vector2.UnitY * 80; }
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
            foreach (Tile t in RandomBoard.CurrentBoard.Tiles)
            {
                if (oneBelow.Intersects(t.CollisionRect) && t.IsBlocked)
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
            Position = RandomBoard.CurrentBoard.WhereCanIGetTo(oldPosition, Position, CollisionRect);
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
