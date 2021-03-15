using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.GUI
{
    public abstract class GameObject
    {
        public Rectangle Bounds { get; set; } 
        public Texture2D Texture { get; set; }

        public int X { get { return Bounds.X; } }
        public int Y { get { return Bounds.Y; } }
        public int Top { get { return Bounds.Top; } }
        public int Bottom { get { return Bounds.Bottom; } }
        public int Right { get { return Bounds.Right; } }
        public int Left { get { return Bounds.Left; } }
        public int Width { get { return Bounds.Width; } }
        public int Height { get { return Bounds.Height; } }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch sb);

    }
}
