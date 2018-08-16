using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Source
{
    public class Camera
    {
        public Matrix TransformMatrix { get; set; }
        public void Follow(Sprite targetSprite)
        {
            Matrix position = Matrix.CreateTranslation(
                -targetSprite.Position.X - (targetSprite.CollisionRect.Width / 2),
                -targetSprite.Position.Y - (targetSprite.CollisionRect.Height / 2),
                0);

            Matrix offset = Matrix.CreateTranslation(
                Constants.ScreenWidth / 2,
                Constants.ScreenHeight / 2,
                0);
            TransformMatrix = position * offset;
        }
    }
}
