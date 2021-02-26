using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TurnBasedStrategy.IO;

namespace TurnBasedStrategy.Managers
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

    }
}
