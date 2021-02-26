using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.IO
{
    public abstract class GameObject
    {
        public Rectangle Bounds { get; set; } 
        public Texture2D Texture { get; set; }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch sb);

    }
}
