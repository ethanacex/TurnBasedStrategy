using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TurnBasedStrategy.IO;
using TurnBasedStrategy.Managers;

namespace TurnBasedStrategy
{
    public class Tile : GameObject
    {
        private Texture2D texture;
        private Texture2D border;
        private Color fill;
        public Color[] PixelInfo { get; set; }

        public Tile(Point position, Texture2D border)
        {
            Bounds = new Rectangle(position.X, position.Y, Configuration.TileWidth, Configuration.TileHeight);
            fill = Configuration.TileColor;
            this.border = border;
            //SetColor(border);
        }

        //public void SetColor(Texture2D texture)
        //{
        //    PixelInfo = new Color[Config.TILEWIDTH * Config.TILEHEIGHT];
        //    texture.GetData(0, Bounds, PixelInfo, 0, PixelInfo.Length);
        //    border.SetData(PixelInfo);
        //}

        public override void Update(GameTime gameTime)
        {
            //if (Bounds.Contains(Input.CurrentMousePosition))
            //{
            //    fill = Color.White;
            //}
        }

        public override void Draw(SpriteBatch sb)
        {
            if (Configuration.ToggleBorder)
            {
                sb.Draw(border, new Rectangle(Bounds.X, Bounds.Y, Configuration.BorderWidth, Bounds.Height + Configuration.BorderWidth), Configuration.BorderColor);
                sb.Draw(border, new Rectangle(Bounds.X, Bounds.Y, Bounds.Width + Configuration.BorderWidth, Configuration.BorderWidth), Configuration.BorderColor);
                sb.Draw(border, new Rectangle(Bounds.X + Bounds.Width, Bounds.Y, Configuration.BorderWidth, Bounds.Height + Configuration.BorderWidth), Configuration.BorderColor);
                sb.Draw(border, new Rectangle(Bounds.X, Bounds.Y + Bounds.Height, Bounds.Width + Configuration.BorderWidth, Configuration.BorderWidth), Configuration.BorderColor);
            }
        }
    }
}
