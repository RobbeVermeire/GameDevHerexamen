using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace PlatformGame.Source
{
    public class Animation
    {
        public List<Rectangle> AnimationFrames { get; set; }
        public Rectangle CurrentRectangle { get; set; }

        public int CurrentFrame { get; set; }

        public int FrameCount { get; private set; }

        public int FrameHeight { get { return Texture.Height/3; } }

        public float FrameSpeed { get; set; }

        public int FrameWidth { get { return Texture.Width / 7; } }

        public bool IsLooping { get; set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount, List<Rectangle> rectangles)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.03f;

            AnimationFrames = new List<Rectangle>(rectangles);

            CurrentRectangle = AnimationFrames[0];

            
        }
        public void AddFrame(Rectangle rectangle)
        {
            AnimationFrames.Add(rectangle);
            CurrentRectangle = AnimationFrames[0];

        }
    }
}
