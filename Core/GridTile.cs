using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using StrategyGame.Managers;
using StrategyGame.Media;

namespace StrategyGame.Core
{
    public class GridTile : GameObject
    {
        public string Text { get; }
        public int Row { get; }
        public int Column { get; }

        private bool highlight = false;
        private Vector2 debugXposition;
        private Vector2 debugYposition;

        public GridTile(Point position)
        {
            Bounds = new Rectangle(position.X, position.Y, Settings.TileWidth, Settings.TileHeight);
        }

        public GridTile(Point position, string label, int column, int row)
        {
            Bounds = new Rectangle(position.X, position.Y, Settings.TileWidth, Settings.TileHeight);
            Text = label;
            Row = row;
            Column = column;
            Texture = GraphicsManager.GetTextureOfColor(Color.White);
            debugXposition = new Vector2(Bounds.Left, Bounds.Bottom - 30);
            debugYposition = new Vector2(Bounds.Left, Bounds.Bottom - 15);

        }

        public override void Update(GameTime gameTime)
        {
            if (Bounds.Contains(Input.CurrentMousePosition))
                highlight = true;
            else
                highlight = false;
        }

        public void DrawSceneToRenderTarget(RenderTarget2D target)
        {
            GraphicsManager.GraphicsDevice.SetRenderTarget(target);
            GraphicsManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch sb = new SpriteBatch(GraphicsManager.GraphicsDevice);

            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            sb.Draw(Texture, Bounds, Color.White * 0.5f);
            sb.End();

            GraphicsManager.GraphicsDevice.SetRenderTarget(null);
        }

        //public void DrawSceneToTexture(ref RenderTarget2D target)
        //{
        //    GraphicsManager.GraphicsDevice.SetRenderTarget(target);
        //    GraphicsManager.GraphicsDevice.Clear(Color.CornflowerBlue);

        //    SpriteBatch sb = new SpriteBatch(GraphicsManager.GraphicsDevice);

        //    sb.Begin();
        //    // Draw to spriteBatch as you normally would

        //    sb.Draw(Texture, Bounds, Color.White);
        //    //if (GameState.DebugMode)
        //    //{
        //    //    sb.DrawString(Fonts.Tile, Text, Bounds.Location.ToVector2(), Color.Black);
        //    //    sb.DrawString(Fonts.Tile, "X: " + Column, debugXposition, Color.Black);
        //    //    sb.DrawString(Fonts.Tile, "Y: " + Row, debugYposition, Color.Black);
        //    //}

        //    sb.End();
        //    //if (GameState.ToggleGridLines)
        //    //    GraphicsManager.DrawGameObjectBorder(sb, this, Settings.BorderWidth, Settings.BorderColor);
        //    GraphicsManager.GraphicsDevice.SetRenderTarget(null);
        //}

        public override void Draw(SpriteBatch sb)
        {
            if (highlight)
            {
                sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                sb.Draw(Texture, Bounds, Color.White * 0.2f);
                sb.End();
            }
            //if (Bounds.Intersects(Settings.ViewFinder))
            //{
                //// Draw tile contents
                //sb.Begin();
                //sb.Draw(Texture, Bounds, Color.White);
                //if (GameState.DebugMode)
                //{
                //    sb.DrawString(Fonts.Tile, Text, Bounds.Location.ToVector2(), Color.Black);
                //    sb.DrawString(Fonts.Tile, "X: " + Column, debugXposition, Color.Black);
                //    sb.DrawString(Fonts.Tile, "Y: " + Row, debugYposition, Color.Black);
                //}
                //sb.End();

                //// Draw tile border
                //if (GameState.ToggleGridLines)
                //    GraphicsManager.DrawGameObjectBorder(sb, this, Settings.BorderWidth, Settings.BorderColor);
            //}
        }
    }
}
