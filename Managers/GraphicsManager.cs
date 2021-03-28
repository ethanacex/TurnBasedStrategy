using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using StrategyGame.GUI;
using StrategyGame.Media;
using StrategyGame.Core;

namespace StrategyGame.Managers
{
    public static class GraphicsManager
    {
        public static event EventHandler<EventArgs> ResolutionChanged;

        public static void OnResolutionChanged()
        {
            ResolutionChanged?.Invoke(null, EventArgs.Empty);
        }

        private static GraphicsDevice Graphics { get; } = ContentService.Instance.Graphics;

        private static GraphicsDeviceManager GraphicsDeviceManager;

        public static void Initialize(GraphicsDeviceManager graphicsDeviceManager)
        {
            GraphicsDeviceManager = graphicsDeviceManager;
        }

        internal static void ToggleFullscreen(object sender, EventArgs e)
        {
            GraphicsDeviceManager.PreferredBackBufferWidth = Graphics.DisplayMode.Width;
            GraphicsDeviceManager.PreferredBackBufferHeight = Graphics.DisplayMode.Height;
            GraphicsDeviceManager.IsFullScreen = true;
            GraphicsDeviceManager.ApplyChanges();
            OnResolutionChanged();
        }

        internal static void ToggleLowRes(object sender, EventArgs e)
        {
            GraphicsDeviceManager.PreferredBackBufferWidth = 1280;
            GraphicsDeviceManager.PreferredBackBufferHeight = 720;
            GraphicsDeviceManager.ApplyChanges();
            OnResolutionChanged();
        }

        internal static void ToggleHighRes(object sender, EventArgs e)
        {
            GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
            GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
            GraphicsDeviceManager.ApplyChanges();
            OnResolutionChanged();
        }

        public static Viewport Viewport { get { return ContentService.Instance.Graphics.Viewport; } }

        public static Texture2D GetTextureOfColor(Color color)
        {
            Texture2D texture = new Texture2D(Graphics, 1, 1, false, SurfaceFormat.Color);
            texture.SetData(new Color[] { color });
            return texture;
        }

        public static void MoveGameObject(GameObject go, Point location)
        {
            go.Bounds = new Rectangle(location, go.Bounds.Size);
        }

        public static void SetGameObjectColor(GameObject go, Color color)
        {
            go.Texture = GetTextureOfColor(color);
        }

        public static Point GetLabelDimensions(Label label)
        {
            return label.Font.MeasureString(label.Body).ToPoint();
        }

        public static Point GetLabelDimensions(string text)
        {
            return GetLabelDimensions(new Label(text, new Vector2(0, 0)));
        }

        public static Point GetLabelDimensions(string text, SpriteFont font)
        {
            return font.MeasureString(text).ToPoint();
        }

        public static void CenterGameObjectX(GameObject go)
        {
            int width = go.Width;
            Rectangle bounds = go.Bounds;
            bounds.X = Viewport.Bounds.Center.X - (width / 2);
            go.Bounds = bounds;
            if (go is Button button)
                button.Label.Bounds = go.Bounds;
        }

        public static Rectangle GetCenterXDrawableRegion(Rectangle bounds)
        {
            int width = bounds.Width;
            Rectangle position = bounds;
            position.X = Viewport.Bounds.Center.X - (width / 2);
            return position;
        }

        public static void CenterObjectOnPanel(GameObject go, Panel panel)
        {
            int width = go.Width;
            Rectangle bounds = go.Bounds;
            bounds.X = panel.Bounds.Center.X - (width / 2);
            go.Bounds = bounds;
            if (go is Button button)
                button.Label.Bounds = go.Bounds;
        }

        public static void DrawGameObjectBorder(SpriteBatch sb, GameObject go, int lineWidth, Color color)
        {
            Texture2D texture = GetTextureOfColor(color);
            sb.Begin();
            sb.Draw(texture, new Rectangle(go.X, go.Y, lineWidth, go.Height + lineWidth), color);
            sb.Draw(texture, new Rectangle(go.X, go.Y, go.Width + lineWidth, lineWidth), color);
            sb.Draw(texture, new Rectangle(go.X + go.Width, go.Y, lineWidth, go.Height + lineWidth), color);
            sb.Draw(texture, new Rectangle(go.X, go.Y + go.Height, go.Width + lineWidth, lineWidth), color);
            sb.End();
        }

    }
}
