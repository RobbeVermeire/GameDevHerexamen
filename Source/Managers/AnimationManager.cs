using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformGame.Source.Managers
{
    public class AnimationManager
    {
        public Animation Animation;

        private float _timer;

        public Vector2 Position { get; set; }

        public AnimationManager(Animation animation)
        {
            Animation = animation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Animation.Texture,
                             Position,
                             Animation.CurrentRectangle,
                             Color.White);
        }

        public void Play(Animation animation)
        {
            if (Animation == animation)
                return;

            Animation = animation;

            Animation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            Animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > Animation.FrameSpeed)
            {
                _timer = 0;
                Animation.CurrentRectangle = Animation.AnimationFrames[Animation.CurrentFrame];
                Animation.CurrentFrame++;
                if (Animation.CurrentFrame >= Animation.FrameCount)
                    Animation.CurrentFrame = 0;
            }
        }
    }
}
