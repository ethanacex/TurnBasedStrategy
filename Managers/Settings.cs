using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;
using System;

namespace StrategyGame.Managers
{
    public static class Settings
    {
        public static int TileWidth { get; private set; } = 50;
        public static int TileHeight { get; private set; } = 50;
        public static int GridColumns { get; private set; } = 16;
        public static int GridRows { get; private set; } = 9;
        public static int GridWidth { get; } = TileWidth * GridColumns;
        public static int GridHeight { get; } = TileHeight * GridRows;
        public static int BorderWidth { get; private set; } = 1;

        public static Point GridPosition { get; set; } = new Point(50, 50);
        public static Color TextureColor { get; private set; } = Color.White;
        public static Color BorderColor { get; set; } = Color.Black;
        public static Color TileColor { get; private set; } = Color.Green;
        public static Color Highlight { get; private set; } = Color.White;
        public static Color TextColor { get; internal set; } = Color.White;
        public static Color BackdropColor { get; set; } = Color.Black;
        public static Color GraphicsDeviceColor { get; set; } = Color.Black;

    }
}
