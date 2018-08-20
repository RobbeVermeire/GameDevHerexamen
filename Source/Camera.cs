using Microsoft.Xna.Framework;

namespace PlatformGame.Source
{
    public class Camera
    {
        public Matrix TransformMatrix { get; set; }
        
        public void Follow(Sprite targetSprite)
        {

            Matrix position = Matrix.CreateTranslation(
                -targetSprite.Position.X - (targetSprite.CollisionRect.Width / 2),
                 -700,
                    0);

            Matrix offset = Matrix.CreateTranslation(
                Constants.ScreenWidth / 2,
                Constants.ScreenHeight * (0.60f),
                0);
            TransformMatrix = position * offset;
        }
    }
}
