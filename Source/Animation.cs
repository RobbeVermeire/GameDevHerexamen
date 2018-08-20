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
        private List<AnimationFrame> Frames;
        public AnimationFrame CurrentFrame { get; set; }
        public float AnimationSpeed { get; set; }

        private int counter = 0;

        private double x = 0;
        public double Offset { get; set; }

        private int _totalWidth = 0;

        public Animation()
        {
            Frames = new List<AnimationFrame>();
            AnimationSpeed = 30;
        }

        public void AddFrame(Rectangle rectangle)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
            };

            Frames.Add(newFrame);
            CurrentFrame = Frames[0];
            Offset = CurrentFrame.SourceRectangle.Width;
            foreach (AnimationFrame f in Frames)
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
                if (counter >= Frames.Count)
                    counter = 0;
                CurrentFrame = Frames[counter];
                Offset += CurrentFrame.SourceRectangle.Width;
            }
            if (Offset >= _totalWidth)
                Offset = 0;


        }
    }
}
