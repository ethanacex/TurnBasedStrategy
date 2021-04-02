using Microsoft.Xna.Framework;
using StrategyGame.Core;

namespace StrategyGame.Managers
{
    public static class Settings
    {
        public static int TileWidth { get; } = 50;
        public static int TileHeight { get; } = 50;
        public static int GridColumns { get; set; } = 70;
        public static int GridRows { get; set; } = 60;
        public static int GridWidth { get { return TileWidth * GridColumns; } }
        public static int GridHeight { get { return TileHeight * GridRows; } }
        public static int BorderWidth { get; } = 1;
        public static int NavigationDistance { get; } = 2;

        public static Point GridPosition { get; set; } = Point.Zero;
        public static Color TextureColor { get; } = Color.White;
        public static Color BorderColor { get; set; } = Color.Black;
        public static Color BackdropColor { get; set; } = Color.Black;
        public static Color TileColor { get; } = Color.Green;
        public static Color Highlight { get; } = Color.White;
        public static Color TextColor { get; } = Color.White;
        public static Color GraphicsDeviceColor { get; set; } = Color.Black;

        public static double ViewFinderXPercent { get; set; } = 0.65;
        public static double ViewFinderYPercent { get; set; } = 0.55;
        public static ViewFinder ViewFinder { get; set; }

    }
}
