using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StrategyGame.Managers;
using StrategyGame.Media;

namespace StrategyGame.Core
{
    public class Tile : GameObject
    {
        public string Text { get; }
        public int Row { get; }
        public int Column { get; }

        private Vector2 debugXposition;
        private Vector2 debugYposition;

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
            Texture = GraphicsManager.GetTextureOfColor(Settings.TileColor);
            debugXposition = new Vector2(Bounds.Left, Bounds.Bottom - 30);
            debugYposition = new Vector2(Bounds.Left, Bounds.Bottom - 15);

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            if (Bounds.Intersects(Settings.ViewFinder))
            {
                // Draw tile contents
                sb.Begin();
                sb.Draw(Texture, Bounds, Color.White);
                if (GameState.DebugMode)
                {
                    sb.DrawString(Fonts.Tile, Text, Bounds.Location.ToVector2(), Color.Black);
                    sb.DrawString(Fonts.Tile, "X: " + Column, debugXposition, Color.Black);
                    sb.DrawString(Fonts.Tile, "Y: " + Row, debugYposition, Color.Black);
                }
                sb.End();

                // Draw tile border
                if (GameState.ToggleGridLines)
                    GraphicsManager.DrawGameObjectBorder(sb, this, Settings.BorderWidth, Settings.BorderColor);
            }
        }
    }
}
