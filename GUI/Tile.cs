using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using StrategyGame.IO;
using StrategyGame.Managers;
using StrategyGame.Media;

namespace StrategyGame.GUI
{
    public class Tile : GameObject
    {
        private Texture2D texture;
        private Texture2D border;
        private Color fill;
        public Color[] PixelInfo { get; set; }

        public Tile(Point position)
        {
            Bounds = new Rectangle(position.X, position.Y, Configuration.TileWidth, Configuration.TileHeight);
            fill = Configuration.TileColor;
            border = Textures.Empty;
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
            if (GameState.ToggleGridLines)
                GraphicsManager.DrawGameObjectBorder(sb, this, Configuration.BorderWidth, Configuration.BorderColor);
        }
    }
}
