using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace PlatformGame.Source
{
    public class AnimationFrame
    {
        public Rectangle SourceRectangle { get; set; }
    }

    public class Animation
    {
        private List<AnimationFrame> frames;
        public AnimationFrame CurrentFrame { get; set; }
        public float AnimationSpeed { get; set; }

        private int counter = 0;

        private double x = 0;
        public double Offset { get; set; }

        private int _totalWidth = 0;

        public Animation()
        {
            frames = new List<AnimationFrame>();
            AnimationSpeed = 20;
        }

        public void AddFrame(Rectangle rectangle)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
            };

            frames.Add(newFrame);
            CurrentFrame = frames[0];
            Offset = CurrentFrame.SourceRectangle.Width;
            foreach (AnimationFrame f in frames)
                _totalWidth += f.SourceRectangle.Width;
        }


        public void Update(GameTime gameTime)
        {
            double temp = CurrentFrame.SourceRectangle.Width * ((double)gameTime.ElapsedGameTime.Milliseconds / 1000);

            x += temp;
            if (x >= CurrentFrame.SourceRectangle.Width / AnimationSpeed)
            {
                Console.WriteLine(x);
                x = 0;
                counter++;
                if (counter >= frames.Count)
                    counter = 0;
                CurrentFrame = frames[counter];
                Offset += CurrentFrame.SourceRectangle.Width;
            }
            if (Offset >= _totalWidth)
                Offset = 0;


        }
    }
}
