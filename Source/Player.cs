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

        private const float InitialJumpForce = -240f;
        private Vector2 jumpForce;
        public Player(Texture2D tex, Vector2 pos, SpriteBatch batch) : base(tex, pos, batch)
        {
            jumpForce = Vector2.Zero;
            Acceleration = Constants.Gravity;

        }

        public void Update(GameTime gameTime)
        {
            CheckKeyBoardStateAndUpdateMovement();
            //AffectWithGravity();
            SimulateFriction();
            MoveIfPossible(gameTime);
            System.Diagnostics.Debug.WriteLine("Position:" + Position + " Velocity:" + Velocity + " Acceleration:" + Acceleration);
            //System.Diagnostics.Debug.WriteLine(CollisionRect);
            
        }


        private void CheckKeyBoardStateAndUpdateMovement()
        {
            
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left)) { Position -= Vector2.UnitX * 10; }
            if (keyboardState.IsKeyDown(Keys.Right)) { Position += Vector2.UnitX * 10; }
            //if (keyboardState.IsKeyDown(Keys.Up)) { jumpForce.Y = InitialJumpForce; }
            if (keyboardState.IsKeyDown(Keys.Up)) { Position -= Vector2.UnitY * 30; }
            //if (keyboardState.IsKeyDown(Keys.Down)) { Speed += Vector2.UnitY * 10; }
        }

        public bool IsOnFirmGround()
        {
            Rectangle oneBelow = CollisionRect;
            oneBelow.Offset(0, 1);
            foreach(Tile t in Board.CurrentBoard.Tiles)
            {
                if(oneBelow.Intersects(t.CollisionRect) && t.IsBlocked)
                {
                    System.Diagnostics.Debug.WriteLine("OP GROND!");
                    return true;
                }
             
            }
            return false;
        }
     
        /// <summary>
        /// Helper functies voor betere code:
        /// </summary>


        private void AffectWithGravity()
        {
            Acceleration = Vector2.UnitY * 10f;
        }
 
        private void SimulateFriction()
        {          
                Velocity -= Velocity * new Vector2(.1f, .1f);  
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

            //Vector2 netForce = jumpForce + Constants.Gravity;


            Position += Velocity * deltaTime / 30; //(Constants.Gravity * deltaTime*deltaTime/2);

            Velocity += Acceleration * deltaTime / 30;

            //jumpForce += Constants.Gravity* deltaTime / 30;

            //System.Diagnostics.Debug.WriteLineIf(IsOnFirmGround(),jumpForce);

            CollisionRect.X = (int)Position.X;
            CollisionRect.Y = (int)Position.Y;

        }
    }
}
