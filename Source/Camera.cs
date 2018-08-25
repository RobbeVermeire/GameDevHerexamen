using Microsoft.Xna.Framework;
using System;

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
        public Player TargetPlayer { get; set; }
        public Camera(Player targetPlayer)
        {
            TargetPlayer = targetPlayer;
            Position = new Vector2(-TargetPlayer.Position.X - (TargetPlayer.CollisionRect.Width / 2), -TargetPlayer.Position.Y - (TargetPlayer.CollisionRect.Height / 2));

        }
        public void FollowSprite()
        {
            Position.X = -TargetPlayer.Position.X - (TargetPlayer.CollisionRect.Width / 2);
            Position.Y = -TargetPlayer.Position.Y - (TargetPlayer.CollisionRect.Height / 2);
        }
        public void MoveRight(int speed)
        {
            Position.X -= speed;
            Position.Y = -TargetPlayer.Position.Y - (TargetPlayer.CollisionRect.Height / 2);

            if((Math.Abs(-Position.X) - Constants.ScreenWidth/2 > Math.Abs(TargetPlayer.Position.X)) ||
                (Math.Abs(-Position.X) + Constants.ScreenWidth / 2 < Math.Abs(TargetPlayer.Position.X)))
            {
                TargetPlayer.Respawn(100, 600);
            }
        }
    }
}
