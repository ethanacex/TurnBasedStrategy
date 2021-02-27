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
        public static Viewport Viewport { get; } = ContentService.Instance.Graphics.Viewport;

        public static Point GetLabelDimensions(Label label)
        {
            Vector2 dimensions = label.Font.MeasureString(label.Body);
            return new Point((int)dimensions.X, (int)dimensions.Y);
        }

        public static void CenterGameObjectOnScreen(GameObject go)
        {
            int width = go.Bounds.Width;
            Rectangle position = go.Bounds;
            position.X = Viewport.Bounds.Center.X - (width / 2);
            go.Bounds = position;
            if (go is Button button)
                button.Label.Bounds = go.Bounds;
        }

        public static void DrawGameObjectBorder(SpriteBatch sb, GameObject go)
        {
            sb.Draw(Textures.Empty, new Rectangle(go.Bounds.X, go.Bounds.Y, Configuration.BorderWidth, go.Bounds.Height + Configuration.BorderWidth), Configuration.BorderColor);
            sb.Draw(Textures.Empty, new Rectangle(go.Bounds.X, go.Bounds.Y, go.Bounds.Width + Configuration.BorderWidth, Configuration.BorderWidth), Configuration.BorderColor);
            sb.Draw(Textures.Empty, new Rectangle(go.Bounds.X + go.Bounds.Width, go.Bounds.Y, Configuration.BorderWidth, go.Bounds.Height + Configuration.BorderWidth), Configuration.BorderColor);
            sb.Draw(Textures.Empty, new Rectangle(go.Bounds.X, go.Bounds.Y + go.Bounds.Height, go.Bounds.Width + Configuration.BorderWidth, Configuration.BorderWidth), Configuration.BorderColor);
        }

    }
}
