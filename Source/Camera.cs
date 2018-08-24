using Microsoft.Xna.Framework;

namespace PlatformGame.Source
{
    public class Camera
    {
        public Matrix TransformMatrix
        {
            get
            { 
            
                Matrix position = Matrix.CreateTranslation(Position.X,Position.Y,0);

                Matrix offset = Matrix.CreateTranslation(
                    Constants.ScreenWidth / 2,
                    Constants.ScreenHeight * (0.60f),
                    0);

                return position * offset;
            }
        }
        public Vector2 Position;
        public Sprite TargetSprite { get; set; }
        public Camera(Sprite targetSprite)
        {
            TargetSprite = targetSprite;
            Position = new Vector2(-TargetSprite.Position.X - (TargetSprite.CollisionRect.Width / 2), -TargetSprite.Position.Y - (TargetSprite.CollisionRect.Height / 2));

        }
        public void FollowSprite()
        {
            Position.X = -TargetSprite.Position.X - (TargetSprite.CollisionRect.Width / 2);
            Position.Y = -TargetSprite.Position.Y - (TargetSprite.CollisionRect.Height / 2);
        }
        public void MoveRight(int speed)
        {
            Position.X -= speed;
            Position.Y = -TargetSprite.Position.Y - (TargetSprite.CollisionRect.Height / 2);
        }
    }
}
