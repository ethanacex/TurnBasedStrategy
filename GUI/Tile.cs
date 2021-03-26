using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using StrategyGame.GUI;
using StrategyGame.Managers;
using StrategyGame.Media;

namespace StrategyGame.Core
{
    public class Tile : GameObject
    {
        public string Text { get; }
        public int Row { get; }
        public int Column { get; }

        public Tile(Point position)
        {
            Bounds = new Rectangle(position.X, position.Y, Settings.TileWidth, Settings.TileHeight);
        }

        public Tile(Point position, string label, int column, int row)
        {
            Bounds = new Rectangle(position.X, position.Y, Settings.TileWidth, Settings.TileHeight);
            Text = label;
            Row = row;
            Column = column;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            if (Bounds.Intersects(Settings.ViewFinder))
            {
                // Draw tile contents
                sb.Draw(GraphicsManager.GetTextureOfColor(Settings.TileColor), Bounds, Color.White);
                if (GameState.DebugMode)
                {
                    sb.DrawString(Fonts.Tile, Text, Bounds.Location.ToVector2(), Color.Black);
                    sb.DrawString(Fonts.Tile, "X: " + Column, new Vector2(Bounds.Left, Bounds.Bottom - 30), Color.Black);
                    sb.DrawString(Fonts.Tile, "Y: " + Row, new Vector2(Bounds.Left, Bounds.Bottom - 15), Color.Black);
                }

                // Draw tile border
                if (GameState.ToggleGridLines)
                    GraphicsManager.DrawGameObjectBorder(sb, this, Settings.BorderWidth, Settings.BorderColor);
            }
        }
    }
}
