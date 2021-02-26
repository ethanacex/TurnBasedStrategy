using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TurnBasedStrategy.Media;

namespace TurnBasedStrategy.Managers
{
    public static class Configuration
    {
        public static bool GameIsRunning { get; set; } = false;
        public static bool WindowInFocus { get; set; } = true;

        public static int TileWidth { get; private set; } = 50;
        public static int TileHeight { get; private set; } = 50;
        public static int GridWidth { get; private set; } = 20;
        public static int GridHeight { get; private set; } = 12;
        public static int BorderWidth { get; private set; } = 1;

        public static Color TextureColor { get; private set; } = Color.White;
        public static Color BorderColor { get; private set; } = Color.Black;
        public static Color TileColor { get; private set; } = Color.White;
        public static Color Highlight { get; private set; } = Color.White;
        public static Color TextColor { get; internal set; } = Color.White;

        public static Texture2D MenuTexture { get; set; } = Scene.Empty;
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
