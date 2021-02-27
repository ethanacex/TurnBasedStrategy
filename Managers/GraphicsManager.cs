using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using StrategyGame.IO;
using StrategyGame.Media;
using StrategyGame.GUI;

namespace StrategyGame.Managers
{
    public static class GraphicsManager
    {
        private static GraphicsDevice Graphics { get; } = ContentService.Instance.Graphics;
        public static Viewport Viewport { get; } = ContentService.Instance.Graphics.Viewport;

        public static Texture2D GetTextureOfColor(Color color)
        {
            Texture2D texture = new Texture2D(Graphics, 1, 1, false, SurfaceFormat.Color);
            texture.SetData(new Color[] { color });
            return texture;
        }

        public static void SetGameObjectColor(GameObject go, Color color)
        {
            go.Texture = GetTextureOfColor(color);
        }

        public static Point GetLabelDimensions(Label label)
        {
            Vector2 dimensions = label.Font.MeasureString(label.Body);
            return dimensions.ToPoint();
        }

        public static Point GetLabelDimensions(string text)
        {
            return GetLabelDimensions(new Label(text, new Vector2(0, 0)));
        }

        public static void CenterGameObjectX(GameObject go)
        {
            int width = go.Bounds.Width;
            Rectangle position = go.Bounds;
            position.X = Viewport.Bounds.Center.X - (width / 2);
            go.Bounds = position;
            if (go is Button button)
                button.Label.Bounds = go.Bounds;
        }

        public static Rectangle GetCenterXRegion(Rectangle bounds)
        {
            int width = bounds.Width;
            Rectangle position = bounds;
            position.X = Viewport.Bounds.Center.X - (width / 2);
            return position;
        }

        public static void DrawGameObjectBorder(SpriteBatch sb, GameObject go, int lineWidth, Color color)
        {
            Texture2D texture = GetTextureOfColor(color);
            sb.Draw(texture, new Rectangle(go.Bounds.X, go.Bounds.Y, lineWidth, go.Bounds.Height + lineWidth), color);
            sb.Draw(texture, new Rectangle(go.Bounds.X, go.Bounds.Y, go.Bounds.Width + lineWidth, lineWidth), color);
            sb.Draw(texture, new Rectangle(go.Bounds.X + go.Bounds.Width, go.Bounds.Y, lineWidth, go.Bounds.Height + lineWidth), color);
            sb.Draw(texture, new Rectangle(go.Bounds.X, go.Bounds.Y + go.Bounds.Height, go.Bounds.Width + lineWidth, lineWidth), color);
        }



    }
}
