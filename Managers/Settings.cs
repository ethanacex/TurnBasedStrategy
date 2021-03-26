using Microsoft.Xna.Framework;

namespace StrategyGame.Managers
{
    public static class Settings
    {
        public static int TileWidth { get; } = 50;
        public static int TileHeight { get; } = 50;
        public static int GridColumns { get; set; } = 100;
        public static int GridRows { get; set; } = 85;
        public static int GridWidth { get { return TileWidth * GridColumns; } }
        public static int GridHeight { get { return TileHeight * GridRows; } }
        public static int BorderWidth { get; } = 1;

        public static Point GridPosition { get; set; } = new Point(50, 50);
        public static Color TextureColor { get; } = Color.White;
        public static Color BorderColor { get; set; } = Color.Black;
        public static Color BackdropColor { get; set; } = Color.Black;
        public static Color TileColor { get; } = Color.Green;
        public static Color Highlight { get; } = Color.White;
        public static Color TextColor { get; } = Color.White;
        public static Color GraphicsDeviceColor { get; set; } = Color.Black;

        public static double ViewFinderXPercent { get; set; } = 0.65;
        public static double ViewFinderYPercent { get; set; } = 0.55;
        public static Rectangle ViewFinder { get; set; }

    }
}
