using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Media;

namespace StrategyGame.Managers
{
    public static class Configuration
    {
        public static bool GameIsRunning { get; set; } = false;
        public static bool DebugColorMode { get; set; } = false;
        public static bool WindowInFocus { get; set; } = true;

        public static int TileWidth { get; private set; } = 50;
        public static int TileHeight { get; private set; } = 50;
        public static int GridColumns { get; private set; } = 16;
        public static int GridRows { get; private set; } = 9;
        public static int GridWidth { get; } = TileWidth * GridColumns;
        public static int GridHeight { get; } = TileHeight * GridRows;
        public static int BorderWidth { get; private set; } = 1;

        public static Color TextureColor { get; private set; } = Color.White;
        public static Color BorderColor { get; private set; } = Color.Black;
        public static Color TileColor { get; private set; } = Color.White;
        public static Color Highlight { get; private set; } = Color.White;
        public static Color TextColor { get; internal set; } = Color.White;

        public static Texture2D BlackTexture { get; set; } = Scene.Empty;
        public static Texture2D DebugTexture { get; set; } = Scene.TestPink;
        public static Texture2D LabelTexture { get; set; } = Scene.Empty;


        private static bool toggleBorder = true;
        public static bool ToggleBorder
        {
            get { return toggleBorder; }
            set
            {
                toggleBorder = value;
                BorderColor = toggleBorder ? Color.Black : Color.Transparent;
            }
        }


    }
}
