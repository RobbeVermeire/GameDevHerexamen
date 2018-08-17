using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source
{
    public abstract class Board
    {
        public Board(Texture2D tex, SpriteBatch batch)
        {
            TileTexture = tex;
            SpriteBatch = batch;
            CurrentBoard = this;
        }
        public Tile[,] Tiles { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        protected const int tileWidth = 70;
        protected const int tileHeight = 70;

        protected SpriteBatch SpriteBatch { get; set; }
        public static Board CurrentBoard { get; protected set; }
        public Texture2D TileTexture { get; set; }


        public Vector2 WhereCanIGetTo(Vector2 originalPosition, Vector2 destination, Rectangle boundingRectangle)
        {
            Vector2 movementToTry = destination - originalPosition;
            Vector2 furthestAvailableLocationSoFar = originalPosition;
            int numberOfStepsToBreakMovementInto = (int)(movementToTry.Length() * 2) + 1;
            Vector2 oneStep = movementToTry / numberOfStepsToBreakMovementInto;

            for (int i = 1; i <= numberOfStepsToBreakMovementInto; i++)
            {
                Vector2 positionToTry = originalPosition + oneStep * i;
                Rectangle newBoundary = CreateRectangleAtPosition(positionToTry, boundingRectangle.Width, boundingRectangle.Height);
                if (HasRoomForRectangle(newBoundary)) { furthestAvailableLocationSoFar = positionToTry; }
                else
                {
                    bool isDiagonalMove = movementToTry.X != 0 && movementToTry.Y != 0;
                    if (isDiagonalMove)
                    {
                        int stepsLeft = numberOfStepsToBreakMovementInto - (i - 1);

                        Vector2 remainingHorizontalMovement = oneStep.X * Vector2.UnitX * stepsLeft;
                        Vector2 finalPositionIfMovingHorizontally = furthestAvailableLocationSoFar + remainingHorizontalMovement;
                        furthestAvailableLocationSoFar =
                            WhereCanIGetTo(furthestAvailableLocationSoFar, finalPositionIfMovingHorizontally, boundingRectangle);

                        Vector2 remainingVerticalMovement = oneStep.Y * Vector2.UnitY * stepsLeft;
                        Vector2 finalPositionIfMovingVertically = furthestAvailableLocationSoFar + remainingVerticalMovement;
                        furthestAvailableLocationSoFar =
                            WhereCanIGetTo(furthestAvailableLocationSoFar, finalPositionIfMovingVertically, boundingRectangle);
                    }
                    break;
                }
            }
            return furthestAvailableLocationSoFar;
        }
        public bool HasRoomForRectangle(Rectangle rectangleToCheck)
        {
            foreach (var tile in Tiles)
            {
                    if (tile.IsBlocked && tile.CollisionRect.Intersects(rectangleToCheck))
                    {
                        return false;
                    }           
            }
            return true;
        }
        private Rectangle CreateRectangleAtPosition(Vector2 positionToTry, int width, int height)
        {
            return new Rectangle((int)positionToTry.X, (int)positionToTry.Y, width, height);
        }

        public void Draw()
        {
            foreach (var tile in Tiles)
            {
                tile.Draw();
            }
        }

    }
}
