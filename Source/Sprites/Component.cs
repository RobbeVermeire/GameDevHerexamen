using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformGame.Source
{
    public abstract class Component
    {
        public abstract SpriteBatch SpriteBatch { get; set; }
        public abstract void Draw();
        public abstract void Update(GameTime gameTime);
    }
}
