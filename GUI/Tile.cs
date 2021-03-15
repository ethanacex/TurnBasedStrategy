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
        public Tile(Point position)
        {
            Bounds = new Rectangle(position.X, position.Y, Settings.TileWidth, Settings.TileHeight);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            // Draw tile fill color
            sb.Draw(GraphicsManager.GetTextureOfColor(Settings.TileColor), Bounds, Color.White);

            // Draw tile border
            if (GameState.ToggleGridLines)
                GraphicsManager.DrawGameObjectBorder(sb, this, Settings.BorderWidth, Settings.BorderColor);
        }
    }
}
